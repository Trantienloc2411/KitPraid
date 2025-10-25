import './App.css'
import { Routes, Route } from 'react-router-dom'
import { Footer } from './components/Footer/Footer'
import Header from './components/Header'
import Home from './pages/Home'
import AuthRoutes from './routes/AuthRoutes'
import { Toaster } from './components/ui/toaster'

function App() {
  return (
    <>
      <Header />
      <main className="main-content">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/auth/*" element={<AuthRoutes />} />
          {/* Legacy redirects to new /auth routes */}
          <Route path="/signin" element={<AuthRoutes />} />
          <Route path="/signup" element={<AuthRoutes />} />
        </Routes>
      </main>
      <Footer />
      <Toaster />
    </>
  )
}

export default App
