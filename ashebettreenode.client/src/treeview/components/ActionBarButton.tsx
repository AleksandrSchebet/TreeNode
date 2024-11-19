import React from "react";

interface Props {
    icon: string;
    tooltip: string;
    disabled: boolean;
    onClick: React.MouseEventHandler<HTMLButtonElement>;
}

function ActionBarButton (props: Props) {
    return (
        <button
            className="btn btn-icon-only btn-text-dark"
            data-toggle="tooltip"
            data-placement="top"
            disabled={props.disabled}
            title={props.tooltip}
            onClick={props.onClick}
        >
            <i className="material-icons">{props.icon}</i>
        </button>
    )
}
export default ActionBarButton;