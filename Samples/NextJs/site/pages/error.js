import cookies from 'next-cookies'
import Link from 'next/link'
import Layout from '../components/layout.js'

const ErrorPage = ({ url: { query: { err, msg }}, user}) => (
  <Layout user={user}>
    <h2 style={{ color: 'red' }}>{ `Error: ${err}` }</h2>
    <p>{ msg }</p>
  </Layout>
)

ErrorPage.getInitialProps = async function(ctx) {
  return {
    user: JSON.parse(cookies(ctx).user || null)
  }
}

export default ErrorPage
