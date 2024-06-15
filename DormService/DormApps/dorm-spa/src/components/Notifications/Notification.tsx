import { useState } from "react";
import { useMount } from "react-use";
import StringUtils from "../../Utils/StringUtils";

type NotificationProps = {
    text: string;
    type: NotificationType;
    onRemove?: () => void;
};

export enum NotificationType {
    Error = "Error",
    Success = "Success"
}

export const Notification: React.FC<NotificationProps> = ({ text, type, onRemove }) => {
    const [showNotification, setShowNotification] = useState<boolean>(false);

    useMount(() => {
        setShowNotification(true);
        setTimeout(() => {
            setShowNotification(true);
            onRemove && onRemove();
        }, 4000);
    });

    return (
        <>
        {showNotification ? <div className={["notification", StringUtils.Uncapitalzie(type.toString())].join(" ")}>
            <div className="shadow">
                <p>{text}</p>
            </div>
        </div> : null}
        </>
    )
}
