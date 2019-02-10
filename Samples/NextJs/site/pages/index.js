import cookies from 'next-cookies'
import Link from 'next/link'
import Layout from '../components/layout.js'

const IndexPage = ({user}) => (
  <Layout user={user}>
    <p>
    Hello, this is an example project using <a href="https://www.microsoft.com/en-us/security/partnerships/graph-security-api">Microsoft Graph Security API</a> with ZEIT <a href="https://now.sh">Now</a> and <a href="https://nextjs.org/">Next.js</a>.
    </p>
  </Layout>
)

IndexPage.getInitialProps = async function(ctx) {
  return {
    user: JSON.parse(cookies(ctx).user || null)
  }
}

export default IndexPage
