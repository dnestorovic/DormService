import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { PageLayout } from '../components/PageLayout/PageLayout';

import LandingPage from './LandingPage/LandingPage';
import Login from './LandingPage/components/Login';
import CanteenPage from './CanteenPage/CanteenPage';
import PaymentPage from './PaymentPage/PaymentPage';
import LaudnryPage from './LaundryPage/LaudnryPage';
import DocumentationPage from './DocumentationPage/DocumentationPage';

import "./../design/design-ui.scss"


function App() {
  return (
    <BrowserRouter>
        <Routes>
            <Route path="/" element={<LandingPage />} />
            <Route path="/login" element={<Login />} />
            <Route path="/canteen" element={<PageLayout><CanteenPage /></PageLayout>} />
            <Route path="/payments" element={<PageLayout><PaymentPage /></PageLayout>} />
            <Route path="/laundry" element={<PageLayout><LaudnryPage /></PageLayout>} />
            <Route path="/documentation" element={<PageLayout><DocumentationPage /></PageLayout>} />
        </Routes>
    </BrowserRouter>
  );
}

export default App;
