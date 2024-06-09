import { BrowserRouter, Route, Routes } from 'react-router-dom';
import LandingPage from './LandingPage/LandingPage';
import LaudnryPage from './LaundryPage/LaudnryPage';
import { PageLayout } from '../components/PageLayout/PageLayout';
import "./../design/design-ui.scss"

function App() {
  return (
    <BrowserRouter>
        <Routes>
            <Route path="/" element={<LandingPage />} />
            <Route path="/laundry" element={<PageLayout><LaudnryPage /></PageLayout>} />
        </Routes>
    </BrowserRouter>
  );
}

export default App;
