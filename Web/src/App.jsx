import { useState } from 'react'
import './App.css'
import LoginPage from './components/LoginPage'
import ModulePage from './components/ModulePage';

function App() {
  const [logged, setLogged] = useState(false);

  return (
    <>
      {!logged && <LoginPage onLogin={(() => setLogged(true))} />}
      {logged && <ModulePage />}
    </>
  )
}

export default App
