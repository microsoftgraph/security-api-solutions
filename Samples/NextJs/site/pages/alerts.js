import cookies from 'next-cookies'
import Layout from '../components/layout'
import AlertList from '../components/alert-list'

const AlertsPage = (props) => (
  <Layout user={props.user}>
    <h1>Alerts</h1>
    <AlertList />
  </Layout>
)

AlertsPage.getInitialProps = async function(ctx) {
  return {
    user: JSON.parse(cookies(ctx).user || null)
  }
}

export default AlertsPage
