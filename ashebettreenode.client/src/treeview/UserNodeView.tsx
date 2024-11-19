import useRequest from '../helpers/useRequest';
import { createRef, useCallback, useEffect, useState } from 'react';
import { IUserNode } from './IUserNode';
import React from 'react';
import ActionBarButton from './components/ActionBarButton';
import ModalView, { ModalDialogRef } from './components/ModalView';
import RecursiveTreeView, { RecursiveTreeviewRef } from './components/RecursiveTreeView';

const UserNodeView = () => {
    const [rootName, setRootName] = useState("b1992c89-ca61-47b8-92fe-ca5b33ada072");
    const [loading, setLoading] = useState(true);
    const [nodes, setNodes] = useState<IUserNode[]>([]);
    
    const [selected, setSelected] = useState<{ id: number, name: string }>({ id: 0, name: '' })
    const [nodeName, setNodeName] = useState<string>('');

    const treeViewRef = createRef<RecursiveTreeviewRef>();
    const [addModalRef, renameModalRef, deleteModalRef] = [createRef<ModalDialogRef>(), createRef<ModalDialogRef>(), createRef<ModalDialogRef>()];
    const request = useRequest();

    const handleSelection = useCallback((event: React.SyntheticEvent, nodesSelected: number[]) => {
        setSelected({ id: nodesSelected[0] ?? 0, name: (event.target as HTMLElement)?.textContent ?? '' });
    }, []);

    const nodeSelected = selected.id > 0;

    const addConfirmed = async () => {
        if (selected.id > 0 && nodeName?.length > 0) {
            const result = await request.fetch<string>("/api/api.user.tree.node.create", {},
                {
                    params:
                    {
                        treeName: rootName,
                        parentNodeId: selected.id,
                        nodeName: nodeName,
                    }
                });
            if (result.length === 0) {
                addModalRef.current?.closeModal()
                await loadTree();
            } else {
                addModalRef.current?.showError(result)
            }
        }
        else {
            addModalRef.current?.showError("Node not selected or Node Name empty");
        }
    }

    const renameConfirmed = async () => {
        if (selected.id > 0 && nodeName?.length > 0) {
            const result = await request.fetch<string>("/api/api.user.tree.node.rename", {},
                {
                    params:
                    {
                        treeName: rootName,
                        nodeId: selected.id,
                        newNodeName: nodeName,
                    }
                });
            if (result.length === 0) {
                renameModalRef.current?.closeModal()
                setSelected({ ...selected,  name: nodeName })
                await loadTree();
            }
            else {
                renameModalRef.current?.showError(result)
            }
        } else {
            renameModalRef.current?.showError("Node not selected or Node Name empty")
        }
    }

    const deleteConfirmed = async () => {
        const result = await request.fetch<string>("/api/api.user.tree.node.delete", {},
            {   params:
                {
                    treeName: rootName,
                    nodeId: selected.id,
                }
            });
        if (result.length === 0) {
            deleteModalRef.current?.closeModal()
            setSelected({id:0, name:''})
            await loadTree()
        } else {
            deleteModalRef.current?.showError(result)
        }
    }

    async function loadTree() {
        try {
            setLoading(true);
            const result = await request.fetch<IUserNode>(`/api/api.user.tree.get?treeName=${rootName}`, {});
            setNodes([result]);
            setLoading(false);
        }
        catch {
            console.log("feetch failed");
        }
    }

    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    const changeRootName = (newRootName: string) => {
        setRootName(newRootName);
    }

    useEffect(() => {
        loadTree();
        return () => { }
    }, [rootName]);

    return loading ? <p><em>Loading...</em></p> :
        (
            <div style={{ width: "600px" }}>
                <div className="container">
                    <div className="row row-cols-1">
                        <div className="col">
                            <div className="d-flex justify-content-between align-items-center">
                                <em>{nodeSelected && <>Selected: {selected?.name}</>}</em>
                                <div
                                    className="d-flex justify-content-end "
                                    style={{ minHeight: "3rem" }}
                                >
                                    <ActionBarButton icon="add" onClick={() => { addModalRef.current?.openModal() }} disabled={!nodeSelected} tooltip="Add" />
                                    <ActionBarButton icon="edit" onClick={() => { renameModalRef.current?.openModal() }} disabled={!nodeSelected} tooltip="Rename" />
                                    <ActionBarButton icon="delete" onClick={() => { deleteModalRef.current?.openModal() }} disabled={!nodeSelected} tooltip="Delete" />
                                    <ActionBarButton
                                        icon={"expand"}
                                        tooltip={"Expand / Collapse"}
                                        onClick={() => { treeViewRef.current?.toogleNodes() }}
                                        disabled={!nodes || !nodes.length}
                                    />
                                </div>
                            </div>
                        </div>
                        <RecursiveTreeView ref={treeViewRef} nodes={nodes} onNodeSelect={handleSelection} />
                    </div>
                </div>
                <div className="modals">
                    <ModalView ref={addModalRef} onConfirm={addConfirmed} title={`Add User Node To "${selected?.name}" Tree`} buttonLabel={"Add"} showCancel={true}>
                        <input type="text" defaultValue='' onChange={(e) => { setNodeName(e.target.value) }}></input>
                    </ModalView>
                    <ModalView ref={renameModalRef} onConfirm={renameConfirmed} title={`Rename User Node "${selected?.name}"`} buttonLabel={"Rename"} showCancel={true}>
                        <input type="text" defaultValue={selected?.name} onChange={(e) => { setNodeName(e.target.value) }}></input>
                    </ModalView>
                    <ModalView ref={deleteModalRef} onConfirm={deleteConfirmed} title={"Confirm Delete"} message={`Delete the User "${selected?.name}"?`} buttonLabel={"Delete"} showCancel={true}>
                    </ModalView>
                </div>
            </div>

        );
}

export default UserNodeView;