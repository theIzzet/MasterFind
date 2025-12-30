import React, { useState } from 'react';
import { masterService } from '../../services/masterService';
import '../../css/MasterPortfolioForm.css';

const PortfolioForm = ({ availableServices, onSuccess, onCancel }) => {
    const [description, setDescription] = useState('');
    const [serviceId, setServiceId] = useState('');
    const [images, setImages] = useState([]);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!serviceId) {
            setError('Lütfen bir hizmet seçin.');
            return;
        }
        if (images.length === 0) {
            setError('Lütfen en az bir resim yükleyin.');
            return;
        }

        setLoading(true);
        setError('');

        const formData = new FormData();
        formData.append('description', description);
        formData.append('serviceId', serviceId);
        // Çoklu dosyaları aynı anahtarla ekliyoruz
        for (let i = 0; i < images.length; i++) {
            formData.append('images', images[i]);
        }

        try {
            await masterService.addPortfolioItem(formData);
            onSuccess(); // Başarılı olunca ana bileşeni bilgilendir
        } catch (err) {
            const errorMsg = err.response?.data?.Errors?.join(', ') || 'Portfolyo eklenirken bir hata oluştu.';
            setError(errorMsg);
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="portfolio-form-container">
            <form onSubmit={handleSubmit} className="portfolio-form">
                <h3>Yeni Portfolyo Ekle</h3>
                {error && <p className="form-error">{error}</p>}
                
                <div className="form-group">
                    <label htmlFor="serviceId">İlgili Hizmet</label>
                    <select id="serviceId" value={serviceId} onChange={(e) => setServiceId(e.target.value)} required>
                        <option value="">Lütfen bir hizmet seçin...</option>
                        {availableServices.map(s => <option key={s.id} value={s.id}>{s.name}</option>)}
                    </select>
                </div>
                
                <div className="form-group">
                    <label htmlFor="description">Açıklama</label>
                    <textarea
                        id="description"
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                        placeholder="Yaptığınız işi kısaca açıklayın (örn: Beyaz lake mutfak dolabı montajı)"
                        required
                    />
                </div>
                
                <div className="form-group">
                    <label htmlFor="images">Resimler (Çoklu seçim yapabilirsiniz)</label>
                    <input 
                        type="file" 
                        id="images"
                        accept="image/*" 
                        multiple // Çoklu dosya seçimine izin ver
                        onChange={(e) => setImages(e.target.files)} 
                        required
                    />
                </div>

                <div className="form-actions">
                    <button type="submit" className="submit-btn" disabled={loading}>
                        {loading ? 'Ekleniyor...' : 'Ekle'}
                    </button>
                    <button type="button" className="cancel-btn" onClick={onCancel} disabled={loading}>
                        İptal
                    </button>
                </div>
            </form>
        </div>
    );
};

export default PortfolioForm;