# Juganaut

![Game Screenshot](https://github.com/user-attachments/assets/0fb92e87-efd8-4a21-bd1f-7eaaec892102)


A very simple 2D game for kids who want to learn programming without starting a project from scratch.

Documentation is in german since it's meant for german kids. :-) 

## Deutsch

Juganaut ist ein sehr einfaches 2D-Spiel. 

So einfach, dass der Code ohne viel Programmier-Erfahrung verstanden, verändert und erweitert werden kann.

Gedacht ist das Spiel für Programmier-Anfänger, um erste Erfahrungen zu sammeln, ohne ein Projekt komplett von
vorn beginnen zu müssen. Stattdessen gibt es ein Spiel, das bereits fertig spielbar ist, sodass eigene
Erweiterungen und Änderungen direkt erlebbar sind.

Das Originalspiel ist in Kotlin programmiert (das recht ähnlich zu Java ist), aber das hier ist eine Version, die auf die Unity Engine umgebaut wurde.
* Dokumentation zu Kotlin: https://kotlinlang.org/docs/home.html

Viel Spaß beim Hacken :-)

## Installation

Voraussetzung ist nur ein installiertes JDK (Java Development Kit).

Ich empfehle zum Programmieren Android Studio: https://developer.android.com/studio

Um eine Programmier-Umgebung mit diesem Spiel einzurichten:
* Klonen dieses Repositories: `git clone https://github.com/tobrup/Juganaut.git`
    * Dadurch sollte das Repository, d.h. der Programm-Code des Spiels, auf der Festplatte gespeichert werden.
* Importieren in Android Studio über: File → Open → Verzeichnis mit dem lokalen Repository auswählen
* Um die Musik in Android zu hören, noch den Gradle-Task `copyAudio` ausführen mit `./gradlew copyAudio`
* Starten des Spiels in Android Studio: Run → Run "MainKt" oder einfach über den entsprechenden Button in der Toolbar

## Aufbau des Projekts

Der Programm-Code ist in drei Teile aufgeteilt, jeweils ein Ordner für jede Plattform (Android und Desktop) und ein Ordner für den geteilten Code:

* `app/src/androidMain`: Plattformspezifischer Android-Code
* `app/src/desktopMain`: Plattformspezifischer Desktop-Code
  * Enthalten beide nur jeweils den UI und Audio-Code
* `app/src/commonMain`: Plattformübergreifender Code
  * BL (in `app/src/commonMain/kotlin/de/glueckstobi/juganaut/bl`): Business Logic
    * Enthält die gesamte Logik des Spiels mit allen Regeln, möglichen Spiel-Elementen mit ihren Bewegungen etc. 
  * UI (in `app/src/commonMain/kotlin/de/glueckstobi/juganaut/ui/compose`): User Interface
    * Enthält fast die gesamte grafische Benutzer-Oberfläche. Hier wird alles gerendert, d.h. auf den Bildschirm angezeigt.
  * geteilte Ressourcen (in `app/src/commonMain/composeResources`)
    * Enthält Bilder und Audiodateien, die für die Anzeige und den Sound im Spiel benutzt werden 

## Wie geht's weiter?

Das Spiel könnte ein paar neue Features vertragen:

* Wie wär's mit Bomben, die der Benutzer anzünden kann (z.B. mit `Enter`) und die dann nach 3 Runden explodieren?


## Lizenzierung
Die gesamte Musik im Ordner `app/src/commonMain/composeResources/files` ist lizenziert unter <a href="https://creativecommons.org/licenses/by-nc-sa/4.0/">CC BY-NC-SA 4.0</a><img src="https://mirrors.creativecommons.org/presskit/icons/cc.svg" alt="" style="max-width: 1em;max-height:1em;margin-left: .2em;"><img src="https://mirrors.creativecommons.org/presskit/icons/by.svg" alt="" style="max-width: 1em;max-height:1em;margin-left: .2em;"><img src="https://mirrors.creativecommons.org/presskit/icons/nc.svg" alt="" style="max-width: 1em;max-height:1em;margin-left: .2em;"><img src="https://mirrors.creativecommons.org/presskit/icons/sa.svg" alt="" style="max-width: 1em;max-height:1em;margin-left: .2em;">

Der gesamte Code ist lizenziert unter der "MIT License" (siehe `LICENSE`) 
