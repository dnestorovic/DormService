import React, {ReactNode, useEffect, useState} from 'react'
import SideMenu from '../SideMenu/SideMenu'
import { useMount } from 'react-use'
import { useNavigate } from 'react-router-dom'

type Props = {
    coverPhoto: string,
    children : ReactNode
 }


export const PageLayout = (props: Props) => {
  const [showIntro, setShowIntro] = useState<boolean>(false);

  useEffect(() => {
      setShowIntro(true);
      setTimeout(() => {
          setShowIntro(false);
      }, 5600);
  }, [props.coverPhoto]);

  function Capitalize(word : string){
    return word.charAt(0).toUpperCase() + word.slice(1);
  }


  return (
    <div className='page-layout'>
        <SideMenu />
        {showIntro ? <div className={["page-intro", props.coverPhoto].join(" ")}>
          <div className='shadow'>
              <h1 className='title'>{Capitalize(props.coverPhoto)}</h1>
          </div>
        </div> : 
        <div className="page-content">
          <div className='shadow'>
            <div className='child-content'>{props.children}</div>
          </div>
        </div>
        
        }
    </div>
  )
}
