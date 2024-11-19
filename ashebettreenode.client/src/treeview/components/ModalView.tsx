import { forwardRef, useImperativeHandle, useState } from "react";
import { Button, Modal } from "react-bootstrap";

interface Props {
    title: string,
    message?: string,
    buttonLabel?: string,
    showCancel?: boolean,
    onConfirm?: () => Promise<void> | void,
    children?: React.ReactNode | React.ReactNode[];
}

export interface ModalDialogRef {
    openModal: () => void;
    closeModal: () => void;
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    showError: (error?: any) => void;
}

const ModalView = forwardRef<ModalDialogRef, Props>((props, refs) => {
    const [error, setError] = useState<string>();
    const [showModal, setShowModal] = useState<boolean>(false);
    const hideModal = () => {
        setShowModal(false);
    }
    useImperativeHandle(refs, () => ({
        showError(error) {
            setError(error?.data?.message ?? error?.toString());
        },
        openModal() {
            setShowModal(true);
        },
        closeModal() {
            hideModal()
        }

    }));

    return (
        <>
            <Modal show={showModal} onHide={hideModal} centered>
                <Modal.Header closeButton={true}>
                    <Modal.Title>{props.title}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div>{props.message ?? ''}</div>
                    <div>{props.children}</div>
                    <div className="text-danger">{error}</div>
                </Modal.Body>
                <Modal.Footer>
                    {props.showCancel && (<Button variant="secondary" onClick={hideModal}>
                        Cancel
                    </Button>)}
                    <Button variant="primary" onClick={props.onConfirm}>
                        {props.buttonLabel ? props.buttonLabel : "Ok"}
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
})

export default ModalView;