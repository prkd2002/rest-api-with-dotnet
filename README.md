# rest-api-with-dotnetAktienverwaltung API
Diese API wurde mit .NET entwickelt und dient zur Verwaltung von Aktieninformationen. Sie ermöglicht es Benutzern, Informationen über verschiedene Aktien abzurufen, neue Aktien hinzuzufügen, vorhandene zu aktualisieren oder zu löschen. Die API bietet Endpunkte für den Zugriff auf die Daten über HTTP-Methoden.

![image](https://github.com/user-attachments/assets/4469836f-b551-4dc4-bc95-644d5b8af324)


![image](https://github.com/user-attachments/assets/80a5da46-383d-446e-bf47-c75a629450ca)


Funktionen
- Alle Aktien abrufen: Ermöglicht es, eine Liste aller verfügbaren Aktien anzuzeigen.
- Eine spezifische Aktie abrufen: Liefert detaillierte Informationen zu einer Aktie basierend auf ihrer ID.
- Neue Aktie erstellen: Fügt eine neue Aktie zum Bestand hinzu.
- Aktie aktualisieren: Aktualisiert die Daten einer bestehenden Aktie.
- Aktie löschen: Entfernt eine Aktie aus dem Bestand.


Technologie-Stack
- Framework: .NET Core
- Sprache: C#
- Datenbank: SQL Server
- Architektur: RESTful API


Endpunkte für die Aktien
- GET /api/stock/all – Ruft alle verfügbaren Aktien ab.
- GET /api/stock/{id} – Ruft eine spezifische Aktie anhand ihrer ID ab.
- POST /api/stock/createStock – Fügt eine neue Aktie hinzu.
- PUT /api/stock/update/{id} – Aktualisiert die Informationen einer vorhandenen Aktie.
- DELETE /api/stock/deelete/{id} – Löscht eine Aktie.


Endpunkte für die Kommentare (Bonus)
- GET /api/comment/all – Ruft alle verfügbaren Kommentare ab.
- GET /api/comment/{id} – Ruft eine spezifische Kommentare anhand ihrer ID ab.
- POST /api/comment/createComment – Fügt eine neue Kommentare hinzu.
- PUT /api/comment/updateComment/{id} – Aktualisiert die Informationen einer vorhandenen Kommentare.
- DELETE /api/comment/deleteComment/{id} – Löscht eine Kommentare.

Anforderungen
- .NET SDK 6.0 oder höher
- SQL Server
