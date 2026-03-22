export enum PopupType {
    Info = "info",
    Warning = "warning",
    Error = "error",
    Success = "success"
}

export interface PopUpProps {
  id: string
  text: string
  type: PopupType
}

const PopUp:React.FC<PopUpProps> = ({id, text, type}) =>{
    return( 
        <div className={"popup "+type}>
            <p>{text}</p>
        </div>
    )
} 

export default PopUp