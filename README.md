# 2017_10_04-CSharpMeetup-FionaFrontOffice

Folien und Code zum CSharp Meetup - "Event Storming - Lessons learned" https://www.meetup.com/de-DE/Hamburg-C-Net-Meetup/events/241016597/

## Disclaimer
Bezogen auf die vorliegende Beispieldomäne und entspr. Code stellt man sicherlich fest, dass das doch nun alles viel zu kompliziert gebaut wurde. Ja, da das ist richtig. Es ist halt nur ein kleines Beispiel.

Ich glaube die Stärken einer solchen Architektur machen dann Sinn, wenn die Domäne größer und komplexer ist, ansonsten ist es völlig overkill.
An dieser Stelle möchte ich noch einmal zum Ausdruck bringen, wie wichtig es ist, vorab so viel wie möglich über meine fachliche Domäne zu erfahren - damit ich dann besser einschätzen kann, mit was für eine Architektur ich die App hochziehe.

In diesem Sinne: Toi toi und happy coding :-)

### mögliche Gesamt-Architektur der App
![](https://github.com/paulimarcello/2017_10_04-CSharpMeetup-FionaFrontOffice/blob/master/hexagonal.jpg)

### mögliche innere Architektur von Domain
![](https://github.com/paulimarcello/2017_10_04-CSharpMeetup-FionaFrontOffice/blob/master/Arc_Domain_intern.jpg)

### Eventstorming V1 mit entsprechenden Problemen
![](https://github.com/paulimarcello/2017_10_04-CSharpMeetup-FionaFrontOffice/blob/master/Fiona_V1.png)

### Eventstorming V2 mögliche bessere Version
![](https://github.com/paulimarcello/2017_10_04-CSharpMeetup-FionaFrontOffice/blob/master/Fiona_V2.png)

### Eventstorming V3 Hinzufügen einer "private Sandbox", um mögliche Vertipper oder andere Probleme zu kompensieren, aber nicht zu heilen
![](https://github.com/paulimarcello/2017_10_04-CSharpMeetup-FionaFrontOffice/blob/master/Fiona_V3.png)
