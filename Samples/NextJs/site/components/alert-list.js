import React, { Component } from 'react'
import Router from 'next/router'
import fetchAPI from '../lib/fetch-api'

class AlertList extends Component {
  constructor(props) {
    super(props)
    this.state = {
      alerts: []
    }
  }

  componentDidMount() {
    this.tick().catch(console.error)
    this.intervalID = setInterval(
      () => this.tick().catch(console.error),
      10000
    )
  }

  componentWillUnmount() {
    clearInterval(this.intervalID)
  }

  async tick() {
    const {error, alerts = []} = await fetchAPI('/alerts')

    if (error) {
      const url = `/error?err=${error.code}&msg=${error.message}`
      Router.push(url)
    }

    this.setState({
      alerts
    })
  }

  render() {
    return (
      <div>
        <table>
          <thead>
            <tr>
              <th style={{width: '30%'}}>ID</th>
              <th style={{width: '70%'}}>Description</th>
            </tr>
          </thead>
          <tbody className='alert'>
            {this.state.alerts.map((alert) => (
              <tr key={alert.id}>
                <td style={{width: '30%'}}>{alert.id}</td>
                <td style={{width: '70%'}}>{alert.description}</td>
              </tr>
            ))}
          </tbody>
        </table>
        <style jsx>{`
            table {
              border: 0;
              width: 100%;
            }
           .alert tr:hover {
              background: #999;
           }
        `}</style>
      </div>
    )
  }
}

export default AlertList
