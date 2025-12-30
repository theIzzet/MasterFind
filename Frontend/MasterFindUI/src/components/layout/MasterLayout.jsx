import React from 'react';
import { Outlet } from 'react-router-dom';
import Navbar from './Navbar';
import '../../css/MasterLayout.css';

const MasterLayout = () => {
    return (
        // Bu ana div, navbar'ın sabit kalmasını ve içeriğin doğru konumlanmasını sağlar.
        <div className="master-layout-container">
            <Navbar />
            <main className="main-content">
                {/* Alt rotaların (dashboard, create-profile vs.) render edileceği yer */}
                <Outlet /> 
            </main>
        </div>
    );
};
export default MasterLayout;