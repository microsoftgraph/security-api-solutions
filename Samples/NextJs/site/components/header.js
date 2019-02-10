import Link from 'next/link'

const navStyle = {
    borderBottom: '1pt solid #DDD',
    margin: '-15pt',
    marginBottom: 0,
    padding: '10pt'
}

const HLink = ({text, myUrl, style}) => (
    <span>
        <Link href={myUrl}>
            <a style={style}>{text}</a>
        </Link>
        <style jsx>{`
            a {
                color: #999;
                margin-right: 15pt;
                text-decoration: none;
                transition: color 0.2s ease;
                font-size: 12pt;
            }
            a:hover {
                color: #000;
            }
        `}</style>
    </span>
)

const Header = ({user}) => (
    <div style={navStyle}>
        <div style={{display: 'inline-block'}}>
            <HLink text='Home' myUrl='/' />
            <HLink text='Alerts' myUrl='/alerts' />
        </div>
        <div style={{display: 'inline-block', float: 'right'}}>
            {user
                ? (<span>{user.name || 'No name'} <HLink text='Logout' myUrl='/logout' style={{marginRight: 0}} /></span>)
                : (<HLink text='Login with Microsoft' myUrl='/login' style={{marginRight: 0}} />)}
        </div>
    </div>
)

export default Header
