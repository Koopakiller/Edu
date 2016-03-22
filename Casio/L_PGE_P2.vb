'Copyright (c) by Tom Lambert (Koopakiller)
'Zahl = Was wird berechnet? Wert von 1 bis 6
'Punkt 1  = x|y|z
'Mehrere Punkte pxyz, qxyz
'Gerade 1 = x=Axyz+Bxyz
'Ebene 1  = ax+by+cz=D
'r, s, t = Stringvariablen
'u, v = Hilfsvariable
'f = Format für Dezimale Ausgaben
'name = Name für Ein/Ausgabewert
'm, n, k, l, d, u, v = Hilfsvariablen

SetDegree
"Fix5"⇒f

ClrText
Lbl Start
Print "Lage PgE"
Print "P - Punkt"
Print "g - Gerade"
Print "E - Ebene"
Print ""
Print "   P  g  E"
Print " E 1  2  3"
Print " g 4  5  2"
Print " P 6  4  1"
Print ""
Print "0 - Über L_PGE"

Input Zahl

'Punkt erfragen
If Zahl=1 Or Zahl=4 Or Zahl=6
Then
  6 ⇒ g
  "P"⇒name
  Goto InputP
  Lbl g6

  If Zahl=6 
  Then
    x⇒px:y⇒py:z⇒pz
    9 ⇒ g
    "Q"⇒name
    Goto InputP
    Lbl g9
    x⇒qx:y⇒qy:z⇒qz
  IfEnd
IfEnd

'Gerade erfragen
If Zahl=2 Or Zahl=4 Or Zahl=5
Then
  7 ⇒ g
  "g"⇒name
  Goto InputG
  Lbl g7
  
  If Zahl=5
  Then
    Ax⇒gAx : Ay⇒gAy : Az⇒gAz
    Bx⇒gBx : By⇒gBy : Bz⇒gBz
    12 ⇒ g
    "h"⇒name
    Goto InputG
    Lbl g12
    Ax⇒hAx : Ay⇒hAy : Az⇒hAz
    Bx⇒hBx : By⇒hBy : Bz⇒hBz
  IfEnd
IfEnd


'Ebene erfragen
If Zahl=1 Or Zahl=2 Or Zahl=3
Then
  8 ⇒ g
  "E"⇒name
  Goto InputE
  Lbl g8
  
  If Zahl=3
  Then
    a⇒Ea : b⇒Eb : c⇒Ec : D⇒ED
    13 ⇒ g
    "F"⇒name
    Goto InputE
    Lbl g13
    a⇒Fa : b⇒Fb : c⇒Fc : D⇒FD
  IfEnd
IfEnd

ClrText
Switch Zahl
 Case 1
  Print "Punkt - Ebene"
  Print ""
  
  0⇒g
  "P"⇒name
  Goto PrintP
  Lbl g0
  Print ""
  1⇒g
  "E"⇒name
  Goto PrintE
  Lbl g1
  Print ""

  x×a + y×b + z×c - D ⇒ k
  a×a + b×b + c×c ⇒ l
  abs(k/√(l)) ⇒ d

  If d=0 
  Then
    Print "P ∈ E"
    Print "Punkt liegt in Ebene"
  Else
    Print "P ∉ E"
    Print "Punkt liegt nicht in Ebene"
    Print ""
    Print "Abstand"
    ExpToStr k,t
    StrJoin " k = ",t,s
    Print s
    ExpToStr l,t
    StrJoin " l = ",t,s
    Print s

    Print " d = | k : √l |"
    ExpToStr d,t
    StrJoin "   = ",t,s
    Print s
    NumToStr d,f,t
    StrJoin "   ≈ ",t,s
    Print s
  IfEnd

  Goto Ende

 Case 2
  Print "Gerade - Ebene"
  Print ""
  
  2⇒g
  "g"⇒name
  Goto PrintG
  Lbl g2
  Print ""
  3⇒g
  "E"⇒name
  Goto PrintE
  Lbl g3
  Print ""
 
  Ax×a + Ay×b + Az×c - D ⇒ m
  -(Bx×a) - (By×b) - (Bz×c) ⇒ n

  If n=0 
  Then
    If m=0
    Then
      Print "g ∈ E"
      Print ""
      Print "Gerade g liegt in Ebene E"
    Else
      Print "g ‖ E   ∧   g ∉ E"
      Print ""
      Print "Gerade g ist parallel zur Ebene E,"
      Print "liegt aber nicht darin"
  
      Ax×a + Ay×b + Az×c - D ⇒ k
      Ax×Ax + Ay×Ay + Az×Az ⇒ l
      k/√(l) ⇒ d

      Print "Abstand"
      ExpToStr k,t
      StrJoin " k = ",t,s
      Print s
      ExpToStr l,t
      StrJoin " l = ",t,s
      Print s

      Print " d = k:√l"
      ExpToStr d,t
      StrJoin "   = ",t,s
      Print s
      NumToStr d,f,t
      StrJoin "   ≈ ",t,s
      Print s

    IfEnd
  Else
    Print "g ⋂ E = S"
    Print "Gerade g durchsticht die Ebene E im Punkt S"
    Print ""      

    Print "Schnittpunkt:"
    m/n ⇒ l
    Ax + l×Bx ⇒ x
    Ay + l×By ⇒ y
    Az + l×Bz ⇒ z
    ExpToStr x,t
    StrJoin "S( ", t, s
    StrJoin s, " | ", s
    ExpToStr y,t
    StrJoin s, t, s
    StrJoin s, " | ", s
    ExpToStr z,t
    StrJoin s, t, s
    StrJoin s, " )", s
    Print s
    NumToStr x,f,t
    StrJoin "S( ", t, s
    StrJoin s, " | ", s
    NumToStr y,f,t
    StrJoin s, t, s
    StrJoin s, " | ", s
    NumToStr z,f,t
    StrJoin s, t, s
    StrJoin s, " )", s
    Print s

    Print ""
    Print "Schnittwinkel:"
    90-angle([a,b,c],[Bx,By,Bz]) ⇒ ang
    ExpToStr ang,s
    StrJoin "∠ = (",s,s
    StrJoin s,")°",s
    Print s
    NumToStr ang,f,s
    StrJoin "  ≈ ",s,s
    StrJoin s,"°",s
    Print s

  IfEnd

  Goto Ende

 Case 4
  Print "Punkt - Gerade"
  Print ""

  4⇒g
  "P"⇒name
  Goto PrintP
  Lbl g4
  Print ""
  5⇒g
  "g"⇒name
  Goto PrintG
  Lbl g5
  Print ""
 
  norm(crossp([Bx,By,Bz],[x-Ax,y-Ay,z-Az])) ⇒ k
  norm([Bx,By,Bz]) ⇒ l
  abs(k/l) ⇒ d

  If d=0 
  Then
    Print "P ∈ g"
    Print "Punkt liegt in Gerade"
  Else
    Print "P ∉ g"
    Print "Punkt liegt nicht in Gerade"
    Print ""
    Print "Kleinstmöglicher Abstand"
    ExpToStr k,t
    StrJoin " k = ",t,s
    Print s
    ExpToStr l,t
    StrJoin " l = ",t,s
    Print s

    Print " d = | k : l |"
    ExpToStr d,t
    StrJoin "   = ",t,s
    Print s
    NumToStr d,f,t
    StrJoin "   ≈ ",t,s
    Print s
  IfEnd

  Goto Ende

 Case 3
  Print "Ebene - Ebene"
  Print ""

  15⇒g
  Ea⇒a : Eb⇒b : Ec⇒c : ED⇒D 
  "E"⇒name
  Goto PrintE
  Lbl g15
  Print ""

  16⇒g
  Fa⇒a : Fb⇒b : Fc⇒c : FD⇒D 
  "F"⇒name
  Goto PrintE
  Lbl g16
  Print ""

  Ea/Fa⇒u1
  Eb/Fb⇒u2
  Ec/Fc⇒u3

  If u1=u2 And u2=u3
  Then
    'Parallel
    If Fc×(ED/Ec)=FD 
    Then
      'Gemeinsamer Schnittpunkt

      Print "E = F"
      Print "Ebenen E und F sind identisch"

    Else
      Print "E ‖ F   ∧   E ⋂ F = Ø"
      Print "Ebenen E und F sind parallel und nicht identisch"
    IfEnd
  Else
    [Ea,Eb,Ec]⇒ven
    [Fa,Fb,Fc]⇒vfn
    (dotp(vfn,vfn)×ED - dotp(ven,vfn)×FD)  /  (dotp(ven,ven)×dotp(vfn,vfn) - dotp(ven,vfn)^2)⇒v1
    (dotp(ven,ven)×FD - dotp(ven,vfn)×ED)  /  (dotp(ven,ven)×dotp(vfn,vfn) - dotp(ven,vfn)^2)⇒v2

    v1×Ea + v2×Fa ⇒ Ax
    v1×Eb + v2×Fb ⇒ Ay
    v1×Ec + v2×Fc ⇒ Az

    Eb×Fc - Ec×Fb ⇒ Bx
    Ec×Fa - Ea×Fc ⇒ By
    Ea×Fb - Eb×Fa ⇒ Bz

    Print "E ⋂ F = g"
    Print "Ebene E und F schneiden einander in Gerade g"

    17⇒g
    "g"⇒name
    Goto PrintG
    Lbl g17

    Print ""
    Print "Schnittwinkel:"
    angle(ven,vfn)⇒ang
    ExpToStr ang,s
    StrJoin "∠ = (",s,s
    StrJoin s,")°",s
    Print s
    NumToStr ang,f,s
    StrJoin "  ≈ ",s,s
    StrJoin s,"°",s
    Print s

  IfEnd
  Goto Ende

 Case 5
  Print "Gerade - Gerade"
  Print ""

  18⇒g
  gAx⇒Ax : gAy⇒Ay : gAz⇒Az
  gBx⇒Bx : gBy⇒By : gBz⇒Bz  
  "g"⇒name
  Goto PrintG
  Lbl g18
  Print ""

  14⇒g
  hAx⇒Ax : hAy⇒Ay : hAz⇒Az
  hBx⇒Bx : hBy⇒By : hBz⇒Bz 
  "h"⇒name
  Goto PrintG
  Lbl g14
  Print ""

  hBx/gBx⇒u1
  hBy/gBy⇒u2
  hBz/gBz⇒u3

  (gAx-hAx)/hBx⇒v1
  (gAy-hAy)/hBy⇒v2
  (gAz-hAz)/hBz⇒v3
  
  If u1=u2 And U2=u3 
  Then
    'Parallel
    If v1=v2 And V2=v3 
    Then
      Print "g = h"
      Print "Geraden g und h sind identisch"
    Else
      Print "g ‖ h"
      Print "Geraden g und h sind parallel"
    IfEnd  
  Else
    If v1=v2 And v2=v3 
    Then
      Print "g ⋂ h = S"
      Print "Geraden g und h schneiden sich im Punkt S"

      19⇒g
      gAx+gBx×v1⇒x
      gAy+gBy×v2⇒y
      gAz+gBz×v3⇒z
      "S"⇒name
      Goto PrintP
      Lbl g19
    Else
      Print "g ∦ h   ∧   g ⋂ h = Ø"
      Print "Geraden g und h sind windschief"
    IfEnd
  IfEnd

  Goto Ende

 Case 6
  Print "Punkt - Punkt"
  Print ""

  10⇒g
  "P"⇒name
  px⇒x:py⇒y:pz⇒z
  Goto PrintP
  Lbl g10
  11⇒g
  "Q"⇒name
  qx⇒x:qy⇒y:qz⇒z
  Goto PrintP
  Lbl g11

  If px=x And py=y And pz=z 
  Then
    Print "Punkte sind Identisch"
  Else
    Print "Abstand"
    norm([qx-px,qy-py,qz-pz])⇒d
    ExpToStr d,t
    StrJoin " d = ",t,s
    Print s
    
    NumToStr d,f,t
    StrJoin "   ≈ ", t, s
    Print s
  IfEnd

  Goto Ende

 Case 0
  Print "Lage PgE Version 2.0"
  Print ""
  Print "Copyright (c) Tom Lambert (Koopakiller)"
  Print ""
  Print "Fehler gefunden? E-Mail an"
  Print "koopakiller@live.de"
  Goto Ende

 Default:
  ClrText
  Print "Falsche Eingabe!"
  Goto Start
SwitchEnd







'Ausgabemethoden
Lbl PrintP
  ExpToStr x,t
  StrJoin name,"( ",s 
  StrJoin s, t, s
  StrJoin s, " | ", s
  ExpToStr y,t
  StrJoin s, t, s
  StrJoin s, " | ", s
  ExpToStr z,t
  StrJoin s, t, s
  StrJoin s, " )", s
  Print s
Goto gotog

Lbl PrintE
  StrJoin name,": (",s
  ExpToStr a,t
  StrJoin s, t, s
  StrJoin s, ")x + (", s
  ExpToStr b,t
  StrJoin s, t, s
  StrJoin s, ")y + (", s
  ExpToStr c,t
  StrJoin s, t, s
  StrJoin s, ")z = ", s
  ExpToStr D,t
  StrJoin s, t, s
  Print s
Goto gotog

Lbl PrintG
  ExpToStr Ax, u1
  ExpToStr Ay, u2
  ExpToStr Az, u3
  StrLen u1, u1
  StrLen u2, u2
  StrLen u3, u3
  max(u1, max(u2, u3)) ⇒ u
  
  ExpToStr Ax, t
  StrJoin "      |", t, s1
  For 0⇒i to u-u1
    StrJoin s1, " ", s1
  Next
  StrJoin s1, "|   |", s1
  StrJoin name,": x = |",s
  ExpToStr Ay, t
  StrJoin s, t, s2
  For 0⇒i to u-u2
    StrJoin s2, " ", s2
  Next
  StrJoin s2, "| + |", s2
  ExpToStr Az, t
  StrJoin "      |", t, s3
  For 0⇒i to u-u3
    StrJoin s3, " ", s3
  Next
  StrJoin s3, "|   |", s3

  ExpToStr Bx, u1
  ExpToStr By, u2
  ExpToStr Bz, u3
  StrLen u1, u1
  StrLen u2, u2
  StrLen u3, u3
  max(u1, max(u2, u3)) ⇒ u

  ExpToStr Bx, t
  StrJoin s1,t,s1
  For 0⇒i to u-u1
    StrJoin s1, " ", s1
  Next
  StrJoin s1, "|",s1
  ExpToStr By, t
  StrJoin s2,t,s2
  For 0⇒i to u-u2
    StrJoin s2, " ", s2
  Next
  StrJoin s2, "|",s2
  ExpToStr Bz, t
  StrJoin s3,t,s3
  For 0⇒i to u-u3
    StrJoin s3, " ", s3
  Next
  StrJoin s3, "|",s3

  Print s1
  Print s2
  Print s3
Goto gotog


'Input Methoden
Lbl InputP
  ClrText
  Print ""

  StrJoin "Punkt ", name, s
  StrJoin s, " = ( x | y | z )", s
  Print s
  Input x : Input y : Input z
Goto gotog

Lbl InputG
  ClrText
  StrJoin "Gerade ", name, s
  StrJoin s, ":", s
  Print s
  Print "    |Ax|   |Bx|"
  Print "x = |Ay| + |By|"
  Print "    |Az|   |Bz|"
  Input Ax : Input Ay : Input Az
  Input Bx : Input By : Input Bz
Goto gotog

Lbl InputE
  ClrText
  StrJoin "Ebene ",name,s
  StrJoin s,": ax + by + cz = D",s
  Print s
  Input a : Input b : Input c : Input D
Goto gotog


'Return to Label
Lbl gotog
Switch g
 Case 0:Goto g0
 Case 1:Goto g1
 Case 2:Goto g2
 Case 3:Goto g3
 Case 4:Goto g4
 Case 5:Goto g5
 Case 6:Goto g6 
 Case 7:Goto g7
 Case 8:Goto g8 
 Case 9:Goto g9
 Case 10:Goto g10 
 Case 11:Goto g11
 Case 12:Goto g12
 Case 13:Goto g13
 Case 14:Goto g14
 Case 15:Goto g15
 Case 16:Goto g16
 Case 17:Goto g17
 Case 18:Goto g18
 Case 19:Goto g19
 Default
  Goto Ende
SwitchEnd


Lbl Ende

