# coding: utf8  

# Aufgabe vom Übungsblatt 3 EWR für die Buchstaben h-k
# Beschreibung: 
#   Schreiben  Sie  eine  Python-Funktion triangle(a,  b,  c), die überprüft, 
#   ob sich ein Dreieck mit drei Seiten der Längen a, b, c konstruieren lässt 
#   und Umfang und Flächeninhalt des Dreiecks berechnet. Rückgabewert der 
#   Python-Funktion ist eine Liste der drei Werte [bool, umfang, flaeche].
#   Zeichnen Sie ein Dreieck, welches Seiten der gegebenen  Längen 
#   a, b und c hat. Zeichnen Sie mindestens drei weitere Dreiecke 
#   (verschoben, gedreht, gespiegelt).
#
# Autoren: 
# - Tom Lambert (Anmeldename: lambertt), 
# - Philipp Gabain von Kries (Anmeldename: kriesphi)

## Importe

import matplotlib
matplotlib.use("TkAgg")
import numpy as np
import matplotlib.pyplot as plt
from matplotlib.patches import Polygon

## Berechnungen
    
def checkSiteLengths(a, b, c):
    """
      Überprüft ob das Dreieck konstruierbar ist
      Parameter: a:float, b:float, c:float
      Rückgabewerte: True, falls das Dreieck Konstruierbar ist, andernfalls False (Boolean)
    """
    if a + b < c or b + c < a or a + c < b:
        return False
    if a + b == c or b + c == a or a + c == b:
        return False
    return True
    
def getAreaFromSiteLengths(a, b, c):
    """
      Berechnung des Flächeninhalts eines Dreiecks aus den 3 Seiten a, b, c
      Parameter: a:float, b:float, c:float
      Rückgabewerte: Die Fläche des Dreiecks(float)
    """
    
    #Berechnung über Heronsche Flächenformel:
    s = (a + b + c) / 2
    return np.sqrt(s * (s - a) * (s - b) * (s - c))
    
def getPerimeterFromSiteLength(a, b, c):
    """
      Berechnung des Umfangs des Dreiecks aus den 3 Seiten a, b, c
      Parameter: a:float, b:float, c:float
      Rückgabewerte: Der Umfang des Dreiecks (float)
    """
    return a + b + c;

def triangle(a, b, c):
    """
      Bestimmt ob das Dreieck konstruiert werden kann, und ggf. den Umfang und den Flächeninhalt
      Parameter: a:float, b:float, c:float
      Rückgabewerte: Eine Liste mit folgenden Werten:
        [flag      = True, falls das Dreieck konstruierbar ist, andernfalls False,
         Perimeter = Der Umfang des Dreiecks oder 0 falls es nicht konstruierbar ist,
         Area      = Die Fläche des Dreiecks oder 0 falls es nicht konstruierbar ist]
    """
    flag = checkSiteLengths(a, b, c)
    return [ 
            flag,
            getPerimeterFromSiteLength(a, b, c) if flag else 0,
            getAreaFromSiteLengths(a, b, c) if flag else 0
           ]


## Eingabe vom Benutzer
   
def useTestData():
    """
      Fragt den Benutzer ob er selbst Werte eingeben möchte
      Parameter: Keine
      Rückgabewert: True, falls Testdaten benutztwerden sollen; andernfalls False
    """
    x = raw_input("Möchten Sie Test-Daten benutzen? (j/n)\n")
    return x.lower() == "j" or x.lower() == "ja" 
   
def inputFloat(msg):
    """
      Fragt einen float-Wert vom Benutzer ab
      Parameter: Eine anzuzeogende Meldung (als String)
      Rückgabewert: Ein eingegebener float-Wert
    """
    a = raw_input(msg + "\n > ");
    
    #Falsche Eingaben abfangen
    try:
        return float(a)
    except ValueError:
        print("Sie haben keine Kommazahl eingegeben")
        return inputFloat(msg)
    
def inputSiteLength(msg):
    """
      Fragt eine Seitenlänge vom Benutzer ab
      Parameter: Eine anzuzeogende Meldung (als String)
      Rückgabewert: Ein eingegebener float-Wert der eine Mögliche Seitenlänge eines Dreiecks darstellt
    """
    a = inputFloat(msg)
    
    #Korrekte Seitenlänge -> Rückgabe
    if a > 0:
        return a
        
    #Fehlerhafte Seitenlänge
    elif a == 0:
        print("Eine Seite mit einer Länge von 0 ist nicht möglich")
    else:
        print("Eine Seite mit einer negativen Länge ist nicht möglich.")
    return inputFloat(msg)
    
def inputAll():
    """
      Fragt alles notwendige vom Benutzer ab
      Parameter: Keine
      Rückgabewert: Eine Liste von 3 eingegebenen Längen [a:float, b:float, c:float]
    """
    a = inputSiteLength("Länge von Seite a?");
    b = inputSiteLength("Länge von Seite b?");
    c = inputSiteLength("Länge von Seite c?");
    
    return (a, b, c)
    
## Zeichnen (inkl. Punkt-Berechnung)

def getPointsFromSites(a, b, c):
    """
      Bestimmt die Punkte eines Dreiecks aus den Seitenlängen
      Parameter: 3 Seitenlängen a:float, b:float, c:float
      Rückgabewert: Eine Liste von verschobenen Punkten [(x:float, y:float), ...]
    """
    hc = np.sqrt(2 * (a*a*b*b + b*b*c*c + c*c*a*a) - (a*a*a*a + b*b*b*b + c*c*c*c)) / (2*c)
    alpha = np.arccos(hc/b)
    return [(0,0), (c,0), (np.sin(alpha)*b, hc)]
    
def getReflectedPointsAtOrdinateFromPoints(points):
    """
      Spiegelt 3 Punkte an der x-Achse
      Parameter: Eine liste von 3 Punkten [(x:float, y:float), ...]
      Rückgabewert: Eine Liste von gespiegelten Punkten [(x:float, y:float), ...]
    """
    return [(-points[0][0], points[0][1]), (-points[1][0], points[1][1]), (-points[2][0], points[2][1])]
    
def getRotatedPointsFromPoints(points):
    """
      Berechnet die Koordinaten von 3 Punkten, die um 90° im Uhrzeigersinn um den Koordinatenursprung gedreht werden
      Parameter: Eine liste von 3 Punkten [(x:float, y:float), ...]
      Rückgabewert: Eine Liste von gedrehten Punkten [(x:float, y:float), ...]
    """
    return [(points[0][1], -points[0][0]), (points[1][1], -points[1][0]), (points[2][1], -points[2][0])]
    
def getMovedPointsFromPoints(points):
    """
      Berechnet die Koordinaten von 3 Punkten, die um 3 nach rechts und 2 nach oben verschoben werden
      Parameter: Eine liste von 3 Punkten [(x:float, y:float), ...]
      Rückgabewert: Eine Liste von verschobenen Punkten [(x:float, y:float), ...]
    """
    x = 3
    y = 2
    return [
     (points[0][0] + 3, points[0][1] + 2),
     (points[1][0] + 3, points[1][1] + 2), 
     (points[2][0] + 3, points[2][1] + 2)
    ]

def getPolygon(p1, p2, p3, color):
    """
      Erzeugt ein Polygon aus den gegebenen Punkten und mit der angegebenen Farbe
      Parameter: Die Punkte p1:(float, float), p2:(float, float), p3:(float, float) und die Farbe color:string
      Rückgabewert: Ein Polygon aus den gegebenen Parametern
    """
    pts = np.array([p1, p2, p3])
    return Polygon(pts, closed=True, facecolor=color, alpha=0.2)
    
def getLimFromPoints(pointsLists): 
    """
      Bestimmt die größten- und kleinsten X- bzw. Y-Werte aller Punkte
      Parameter: Eine Liste von Listen von Punkten. [[(x:flaot, y:flaot), ...], ...]
      Rückgabewert: ((xmin:flaot, xmax:flaot), (ymin:flaot, ymax:flaot))
    """
    xmin = xmax = pointsLists[0][0][0]
    ymin = ymax = pointsLists[0][0][1]
    for list in pointsLists:
        for point in list:
            xmin = min(xmin, point[0])
            ymin = min(ymin, point[1])
            xmax = max(xmax, point[0])
            ymax = max(ymax, point[1])
    return ((xmin, xmax), (ymin, ymax))

def drawTriangles(a, b, c):   
    """
      Zeichnet das Dreieck sowie eine gedrehte-, eine verschobene- und eine gespiegelte Version
      Parameter: Seitenlängen: a:float, b:float, c:float
      Rückgabewert: Keiner
    """
    ax = plt.gca()
    
    #Zeichnen
    points = getPointsFromSites(a, b, c)
    ax.add_patch(getPolygon(points[0], points[1], points[2], "#FF0000"))   
     
    reflectedPoints = getReflectedPointsAtOrdinateFromPoints(points)
    ax.add_patch(getPolygon(reflectedPoints[0], reflectedPoints[1], reflectedPoints[2], "#00FF00"))
    
    rotatedPoints = getRotatedPointsFromPoints(points)
    ax.add_patch(getPolygon(rotatedPoints[0], rotatedPoints[1], rotatedPoints[2], "#0000FF"))
    
    movedPoints = getMovedPointsFromPoints(points)
    ax.add_patch(getPolygon(movedPoints[0], movedPoints[1], movedPoints[2], "#00FFFF"))
    
    #Legenden Hinzufügen
    ax.legend([
      "Original", 
      "Gespiegelt an der Y-Achse", 
      "Gedreht um 90 Grad im Uhrzeigersinn", 
      "Verschoben um (3|2)"
    ])
    
    #Bestimmen und festlegen der Grenzen in denen die Dreiecke liegen
    lims = getLimFromPoints([points, reflectedPoints, rotatedPoints, movedPoints])
    ax.set_xlim(lims[0][0] - 1, lims[0][1] + 1)
    ax.set_ylim(lims[1][0] - 1, lims[1][1] + 1)
    
    #Anzeigen
    _ = plt.show()
    
## Sonstige

def help():
    """
      Zeigt eine kurze Hilfe für den Benutzer an
      Parameter: Keine
      Rückgabewert: Keiner
    """
    print("Dieses Programm berechnet Flächeninhalt- und Umfang von Dreiecken und kann dieses zeichnen.")
    print("Hierfür werden sie nacheinander nach den 3 Seitenlängen a, b und c gefragt.")
    print("Geben Sie diese Seitenlängen ohne Einheit und mit einem . als Dezimaltrennzeichen ein.")
    print("Alternativ können Sie auch Test-Daten verwenden, die vom Entwickler vorgegeben wurden.")
    
def printLine():
    """
      Gibt eine Linie aus = in die Konsole aus
      Parameter: Keine
      Rückgabewert: Keiner
    """
    print("====================================================")
    
## Test

def testData():
    """
      Zeigt die Test-Daten dem Benutzer an und gibt diese als Tupel zurück
      Parameter: Keine
      Rückgabewert: 3 Seitenlängen als Tupel (a:float, b:float, c:float)
    """
    print("Die Test-Daten sind:")
    print(" a = 2.0")
    print(" b = 3.0")
    print(" c = 1.5")
    return (2.0, 3.0, 1.5)
    
def test():
    """
      Testet die Elementaren Funktionen des Programms und gibt auftretende Fehler aus
      Parameter: Keine
      Rückgabewert: Keiner
    """
    print("Anzeige der Kurz-Hilfe:")
    help()
    printLine()
    
    print("Funktion: checkSiteLengths")
    if(checkSiteLengths(1,1,1) != True): print("Fehler bei (1,1,1)")
    if(checkSiteLengths(1,2,3) != False): print("Fehler bei (1,2,3)")
    if(checkSiteLengths(15,5,5) != False): print("Fehler bei (15,5,5)")
    printLine()
    
    print("Funktion: getAreaFromSiteLengths")
    if(getAreaFromSiteLengths(3,4,5) != 6): print("Fehler bei (3,4,5)")
    printLine()
    
    print("Funktion: getPerimeterFromSiteLength")
    if(getPerimeterFromSiteLength(3,4,5) != 12): print("Fehler bei (3,4,5)")
    printLine()
    
    print("Funktion: triangle")
    if(triangle(10,1,1) != [False, 0, 0]): print("Fehler bei (10,1,1)")
    if(triangle(3,4,5) != [True, 12, 6]): print("Fehler bei (3,4,5)")
    printLine()    
    
    print("Komplette programm Ausführung mit Test-Daten die ein nicht konstruierbares Dreieck liefern")
    tri = triangle(10, 1, 1)
    if not tri[0]:
        print("Das Dreieck ist nicht konstruierbar")
    else:
        print("Fehler")
    printLine()    
    
    print("Komplette programm Ausführung mit Test-Daten")
    abc = testData()
    tri = triangle(abc[0], abc[1], abc[2])
    if not tri[0]:
        print("Das Dreieck ist nicht konstruierbar")
    else:
        print("Der Umfang beträgt " + str(tri[1]))
        print("Der Flächeninhalt beträgt " + str(tri[2]))
        drawTriangles(abc[0], abc[1], abc[2])
    
## Hauptprogramm

def main():
    """
      Das Haupt-Programm, weg gekapselt in einer Funktion
      Parameter: Keine
      Rückgabewert: Keiner
    """
    if(__name__ == "__main__"):
        print("Es wurde __name__ == '__main__' übergeben.");
        print("Es werden Experimente durchgeführt.");
        test()
    else:
        help()
        if(useTestData()):
            abc = testData()
        else:
            abc = inputAll()
        tri = triangle(abc[0], abc[1], abc[2])
        if not tri[0]:
            print("Das Dreieck ist nicht konstruierbar")
        else:
            print("Der Umfang beträgt " + str(tri[1]))
            print("Der Flächeninhalt beträgt " + str(tri[2]))
            drawTriangles(abc[0], abc[1], abc[2])

main();
