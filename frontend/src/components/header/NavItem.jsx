import {NavLink} from "react-router-dom";

export default function NavItem({to, children}) {
    return (
        <NavLink to={to} className="text-black hover:text-red-500">
            {children}
        </NavLink>
    )
}