$(document).ready(function () {
    $('form').on('submit', function (e) {
        e.preventDefault();
        
        $.ajax({
            url: $(this).attr('action'),
            method: 'POST',
            data: $(this).serialize(),
            success: function (response) {
                if (response.basarili) {
                    let html = '<div class="mt-4"><h3>Size Önerilen Araçlar</h3>';
                    
                    response.oneriler.forEach(function (oneri) {
                        html += `
                            <div class="card mb-3">
                                <div class="card-body">
                                    <h5 class="card-title">${oneri.marka} ${oneri.model}</h5>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <p><strong>Yıl:</strong> ${oneri.yil}</p>
                                            <p><strong>Fiyat:</strong> ${oneri.fiyat.toLocaleString('tr-TR')} TL</p>
                                            <p><strong>Kilometre:</strong> ${oneri.kilometre.toLocaleString('tr-TR')} km</p>
                                            <p><strong>Yakıt Tipi:</strong> ${oneri.yakitTipi}</p>
                                        </div>
                                        <div class="col-md-6">
                                            <p><strong>Vites Tipi:</strong> ${oneri.vitesTipi}</p>
                                            <p><strong>Motor Hacmi:</strong> ${oneri.motorHacmi}</p>
                                            <p><strong>Beygir Gücü:</strong> ${oneri.beygirGucu} HP</p>
                                            <p><strong>Uygunluk Puanı:</strong> ${oneri.uygunlukPuani}%</p>
                                        </div>
                                    </div>
                                    <div class="mt-3">
                                        <h6>Karşılanan Kriterler:</h6>
                                        <ul>
                                            ${oneri.karsilananKriterler.map(kriter => `<li>${kriter}</li>`).join('')}
                                        </ul>
                                    </div>
                                    <a href="/Arac/Index/${oneri.id}" class="btn btn-primary mt-2">Detayları Gör</a>
                                </div>
                            </div>
                        `;
                    });
                    
                    html += '</div>';
                    
                    // Önceki sonuçları temizle
                    $('#sonuclar').remove();
                    
                    // Yeni sonuçları ekle
                    $('.card-body').append('<div id="sonuclar">' + html + '</div>');
                    
                    // Form'u gizle
                    $('form').hide();
                    
                    // Yeni arama butonu ekle
                    $('.card-body').append('<div class="text-center mt-4"><button class="btn btn-secondary" onclick="location.reload()">Yeni Arama Yap</button></div>');
                } else {
                    alert('Üzgünüz, kriterlere uygun araç bulunamadı.');
                }
            },
            error: function () {
                alert('Bir hata oluştu. Lütfen tekrar deneyin.');
            }
        });
    });
}); 