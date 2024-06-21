import React from 'react';

type DropdownMenuProps = {
  options: string[];
  title?: string;
  selectedValue?: string; // New prop to pass selected value
  onSelect: (option: string) => void; 
}

const DropdownMenu: React.FC<DropdownMenuProps> = ({ title, options, selectedValue, onSelect }) => {
  return (
    <div className='dropdown-menu'>
        <div className="dropdown">
        <div className="dropdown-button">{selectedValue || title || "Select an option"}<span>&#x25BE;</span></div>
        <ul className="dropdown-list">
            {options.map(op => {
              return <li key={op} onClick={() => onSelect(op)}>{op}</li>
            })}
        </ul>
        </div>
    </div>
  )
}

export default DropdownMenu;
