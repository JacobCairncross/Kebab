import PopUp, { PopUpProps } from "./PopUp"

interface props {
    popUps: PopUpProps[]
}

const PopUpQueue:React.FC<props> = ({popUps}) => {
    return (
        <div className="popup-queue">
            {popUps.map(popUp =>
                <PopUp key={popUp.id} id={popUp.id} text={popUp.text} type={popUp.type}/>
            )}
        </div>
    )
}

export default PopUpQueue;