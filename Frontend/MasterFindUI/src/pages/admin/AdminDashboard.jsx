import React, { useEffect, useState } from 'react';
import { adminService } from '../../services/adminService';

const AdminDashboard = () => {
    const [stats, setStats] = useState(null);

    useEffect(() => {
        adminService.getDashboardStats().then(res => {
            setStats(res.data);
        });
    }, []);

    return (
        <div>
            <h1>Admin Panel</h1>
            {stats && (
                <pre>{JSON.stringify(stats, null, 2)}</pre>
            )}
        </div>
    );
};

export default AdminDashboard;
