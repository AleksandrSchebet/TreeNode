import { TreeView, TreeViewItem } from "@trimbleinc/modus-react-bootstrap";
import { Fragment } from "react/jsx-runtime";
import { IUserNode } from "../IUserNode";
import { forwardRef, useCallback, useImperativeHandle, useState } from "react";

interface Props {
    nodes: IUserNode[];
    onNodeSelect?: (event: React.SyntheticEvent, selectedNodes: number[], nodeInFocus: number) => void;
}

export interface RecursiveTreeviewRef {
    toogleNodes: () => void;
}

const RecursiveTreeView = forwardRef<RecursiveTreeviewRef, Props>((props, refs) => {
    const [expanded, setExpanded] = useState<number[]>([])
    const handleExpansion = useCallback((event: React.SyntheticEvent, nodesExpanded: number[]) => {
        setExpanded(nodesExpanded);
    }, [])


    useImperativeHandle(refs, () => ({
        toogleNodes() {
            setExpanded(expanded.length === 0 ? getNodeIds(props.nodes) : []);
        },
    }));

    function getNodeIds(nodes: IUserNode[]) {
        return nodes.reduce((r: number[], { id, children }) => {
            r.push(id, ...(children ? getNodeIds(children) : []))
            return r
        }, [])

    }
    return <>
        {// @ts-expect-error:next-line 
            <TreeView id="basic" nodeId={props.nodes?.[0].id} onNodeToggle={handleExpansion} onNodeSelect={props.onNodeSelect} expanded={expanded}>
                <TreeViewChild nodes={props.nodes} />
            </TreeView>
        }
    </>
})

function TreeViewChild(props: { nodes: IUserNode[] }) {
    return <Fragment>
        {props.nodes.map(
            (item, index) => {
                return (
                    <Fragment key={index}>
                        <TreeViewItem nodeId={item.id} key={item.id} data-name={item.name} label={item.name}>
                            {item.children.length > 0 && <TreeViewChild nodes={item.children} />}
                        </TreeViewItem>
                    </Fragment>
                    )
                }
            )
        }
    </Fragment >
}

export default RecursiveTreeView;