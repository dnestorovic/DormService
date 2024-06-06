import React, {ReactNode} from 'react'
import SideMenu from '../SideMenu/SideMenu'

type Props = { 
    children : ReactNode
 }


export const PageLayout = (props: Props) => {
  return (
    <div className='page-layout'>
        <SideMenu />
        <div className='page-content'>{props.children}</div>
    </div>
  )
}
