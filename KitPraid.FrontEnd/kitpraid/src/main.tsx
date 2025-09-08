import { Provider } from "./components/ui/provider"
import { Provider as ReduxProvider } from "react-redux"
import React from "react"
import ReactDOM from "react-dom/client"
import { BrowserRouter } from "react-router-dom"
import App from "./App"
import "./styles/globals.css"
import store from "./redux/store/store"

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <ReduxProvider store={store}>
      <Provider>
        <BrowserRouter>
          <App />
        </BrowserRouter>
      </Provider>
    </ReduxProvider>
  </React.StrictMode>,
)