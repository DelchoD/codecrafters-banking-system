import './App.css'
import 'bootstrap/dist/css/bootstrap.min.css';

import { AuthContextProvider } from './contexts/AuthContext'
import { Route, Routes } from 'react-router-dom'

import AccountCreate from './components/account-create/AccountCreate'
import AccountsMine from './components/accounts-mine/AccountsMine'
import Footer from './components/footer/Footer'
import ForgotPassword from './components/forgot-password/ForgotPassword'
import Header from './components/header/Header'
import Home from './components/home/Home'
import Login from './components/login/Login'
import Profile from './components/profile/Profile'
import Register from './components/register/Register'
import TransactionsMine from './components/transactions-mine/TransactionsMine'
import AccountEdit from './components/account-edit/AccountEdit'


function App() {

  return (

    <AuthContextProvider>

      <Header />

      <main>

        <Routes>

          <Route path='/' element={<Home />} />

          <Route path='/profile' element={<Profile />} />

          <Route path='/login' element={<Login />} />
          <Route path='/register' element={<Register />} />
          <Route path='/forgot-password' element={<ForgotPassword />} />


          <Route path='/account/create' element={<AccountCreate />} />
          <Route path='/account/mine' element={<AccountsMine />} />
          <Route path='/account/:accountId/edit' element={<AccountEdit />} />

          <Route path='/transaction/mine' element={<TransactionsMine />} />


        </Routes>

      </main>


      <Footer />

    </AuthContextProvider>
  )
}

export default App
