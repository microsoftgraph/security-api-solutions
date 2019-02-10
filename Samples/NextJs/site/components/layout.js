import Header from './header'

const layoutStyle = {
  margin: '12pt',
  padding: '15pt',
  border: '1pt solid #DDD'
}

const Layout = (props) => (
  <div style={layoutStyle}>
    <Header user={props.user} />
    {props.children}
  </div>
)

export default Layout
