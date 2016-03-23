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
"Fix5"⇒θf

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
Print "0 - Über L_PGE_P"

Input θZahl,"Was wird berechnet? (Zahl)"

'Punkt erfragen
If θZahl=1 Or θZahl=4 Or θZahl=6
Then
  6 ⇒ θg
  "P"⇒θname
  Goto InputP
  Lbl g6

  If θZahl=6 
  Then
    θx⇒θpx:θy⇒θpy:θz⇒θpz
    9 ⇒ θg
    "Q"⇒θname
    Goto InputP
    Lbl g9
    θx⇒θqx:θy⇒θqy:θz⇒θqz
  IfEnd
IfEnd

'Gerade erfragen
If θZahl=2 Or θZahl=4 Or θZahl=5
Then
  7 ⇒ θg
  "g"⇒θname
  Goto InputG
  Lbl g7
  
  If θZahl=5
  Then
    θAx⇒θgAx : θAy⇒θgAy : θAz⇒θgAz
    θBx⇒θgBx : θBy⇒θgBy : θBz⇒θgBz
    12 ⇒ θg
    "h"⇒θname
    Goto InputG
    Lbl g12
    θAx⇒θhAx : θAy⇒θhAy : θAz⇒θhAz
    θBx⇒θhBx : θBy⇒θhBy : θBz⇒θhBz
  IfEnd
IfEnd


'Ebene erfragen
If θZahl=1 Or θZahl=2 Or θZahl=3
Then
  8 ⇒ θg
  "E"⇒θname
  Goto InputE
  Lbl g8
  
  If θZahl=3
  Then
    θa⇒θEa : θb⇒θEb : θc⇒θEc : θD⇒θED
    13 ⇒ θg
    "F"⇒θname
    Goto InputE
    Lbl g13
    θa⇒θFa : θb⇒θFb : θc⇒θFc : θD⇒θFD
  IfEnd
IfEnd

ClrText
Switch θZahl
 Case 1
  Print "Punkt - Ebene"
  Print ""
  
  0⇒θg
  "P"⇒θname
  Goto PrintP
  Lbl g0
  Print ""
  1⇒θg
  "E"⇒θname
  Goto PrintE
  Lbl g1
  Print ""

  θx×θa + θy×θb + θz×θc - θD ⇒ θk
  θa×θa + θb×θb + θc×θc ⇒ θl
  abs(θk/√(θl)) ⇒ θd

  If θd=0 
  Then
    Print "P ∈ E"
    Print "Punkt liegt in Ebene"
  Else
    Print "P ∉ E"
    Print "Punkt liegt nicht in Ebene"
    Print ""
    Print "Abstand"
    ExpToStr θk,θt
    StrJoin " k = ",θt,θs
    Print θs
    ExpToStr θl,θt
    StrJoin " l = ",t,s
    Print θs

    Print " d = | k : √l |"
    ExpToStr θd,θt
    StrJoin "   = ",θt,θs
    Print θs
    NumToStr θd,θf,θt
    StrJoin "   ≈ ",θt,θs
    Print θs
  IfEnd

  Goto Ende

 Case 2
  Print "Gerade - Ebene"
  Print ""
  
  2⇒θg
  "g"⇒θname
  "λ"⇒θλ
  Goto PrintG
  Lbl g2
  Print ""
  3⇒θg
  "E"⇒θname
  Goto PrintE
  Lbl g3
  Print ""
 
  θAx×θa + θAy×θb + θAz×θc - θD ⇒ θm
  -(θBx×θa) - (θBy×θb) - (θBz×θc) ⇒ θn

  If θn=0 
  Then
    If θm=0
    Then
      Print "g ∈ E"
      Print ""
      Print "Gerade g liegt in Ebene E"
    Else
      Print "g ‖ E   ∧   g ∉ E"
      Print ""
      Print "Gerade g ist parallel zur Ebene E,"
      Print "liegt aber nicht darin"
  
      θAx×θa + θAy×θb + θAz×θc - θD ⇒ θk
      θAx×θAx + θAy×θAy + θAz×θAz ⇒ θl
      θk/√(θl) ⇒ θd

      Print "Abstand"
      ExpToStr θk,θt
      StrJoin " k = ",θt,θs
      Print θs
      ExpToStr θl,θt
      StrJoin " l = ",θt,θs
      Print θs

      Print " d = k:√l"
      ExpToStr θd,θt
      StrJoin "   = ",θt,θs
      Print θs
      NumToStr θd,θf,θt
      StrJoin "   ≈ ",θt,θs
      Print θs

    IfEnd
  Else
    Print "g ⋂ E = S"
    Print "Gerade g durchsticht die Ebene E im Punkt S"
    Print ""      

    Print "Schnittpunkt:"
    θm/θn ⇒ θl
    θAx + θl×θBx ⇒ θx
    θAy + θl×θBy ⇒ θy
    θAz + θl×θBz ⇒ θz
    ExpToStr θx,θt
    StrJoin "S( ", θt, θs
    StrJoin θs, " | ", θs
    ExpToStr θy,θt
    StrJoin θs, θt, θs
    StrJoin θs, " | ", θs
    ExpToStr θz,θt
    StrJoin θs, θt, θs
    StrJoin θs, " )", θs
    Print θs
    NumToStr θx,θf,θt
    StrJoin "S( ", θt, θs
    StrJoin θs, " | ", θs
    NumToStr θy,θf,θt
    StrJoin θs, θt, θs
    StrJoin θs, " | ", θs
    NumToStr θz,θf,θt
    StrJoin θs, θt, θs
    StrJoin θs, " )", θs
    Print θs

    Print ""
    Print "Schnittwinkel:"

    22⇒θg
    90-angle([θa,θb,θc],[θBx,θBy,θBz])⇒θang
    Goto PrintAng
    Lbl g22

  IfEnd

  Goto Ende

 Case 4
  Print "Punkt - Gerade"
  Print ""

  4⇒θg
  "P"⇒θname
  Goto PrintP
  Lbl g4
  Print ""
  5⇒θg
  "g"⇒θname
  "λ"⇒θλ
  Goto PrintG
  Lbl g5
  Print ""
 
  norm(crossp([θBx,θBy,θBz],[θx-θAx,θy-θAy,θz-θAz])) ⇒ θk
  norm([θBx,θBy,θBz]) ⇒ θl
  abs(θk/θl) ⇒ θd

  If d=0 
  Then
    Print "P ∈ g"
    Print "Punkt liegt in Gerade"
  Else
    Print "P ∉ g"
    Print "Punkt liegt nicht in Gerade"
    Print ""
    Print "Kleinstmöglicher Abstand"
    ExpToStr θk,θt
    StrJoin " k = ",θt,θs
    Print θs
    ExpToStr θl,θt
    StrJoin " l = ",θt,θs
    Print θs

    Print " d = | k : l |"
    ExpToStr θd,θt
    StrJoin "   = ",θt,θs
    Print θs
    NumToStr θd,θf,θt
    StrJoin "   ≈ ",θt,θs
    Print θs
  IfEnd

  Goto Ende

 Case 3
  Print "Ebene - Ebene"
  Print ""

  15⇒θg
  θEa⇒θa : θEb⇒θb : θEc⇒θc : θED⇒θD 
  "E"⇒θname
  Goto PrintE
  Lbl g15
  Print ""

  16⇒θg
  θFa⇒θa : θFb⇒θb : θFc⇒θc : θFD⇒θD 
  "F"⇒θname
  Goto PrintE
  Lbl g16
  Print ""

  θEa/θFa⇒θu1
  θEb/θFb⇒θu2
  θEc/θFc⇒θu3

  If θu1=θu2 And θu2=θu3
  Then
    'Parallel
    If θFc×(θED/θEc)=θFD 
    Then
      'Gemeinsamer Schnittpunkt

      Print "E = F"
      Print "Ebenen E und F sind identisch"

    Else
      Print "E ‖ F   ∧   E ⋂ F = Ø"
      Print "Ebenen E und F sind parallel und nicht identisch"
    IfEnd
  Else
    [θEa,θEb,θEc]⇒θven
    [θFa,θFb,θFc]⇒θvfn
    (dotp(θvfn,θvfn)×θED - dotp(θven,θvfn)×θFD)  /  (dotp(θven,θven)×dotp(θvfn,θvfn) - dotp(θven,θvfn)^2)⇒θv1
    (dotp(θven,θven)×θFD - dotp(θven,θvfn)×θED)  /  (dotp(θven,θven)×dotp(θvfn,θvfn) - dotp(θven,θvfn)^2)⇒θv2

    θv1×θEa + θv2×θFa ⇒ θAx
    θv1×θEb + θv2×θFb ⇒ θAy
    θv1×θEc + θv2×θFc ⇒ θAz

    θEb×θFc - θEc×θFb ⇒ θBx
    θEc×θFa - θEa×θFc ⇒ θBy
    θEa×θFb - θEb×θFa ⇒ θBz

    Print "E ⋂ F = g"
    Print "Ebene E und F schneiden einander in Gerade g"

    17⇒θg
    "g"⇒θname
    "λ"⇒θλ
    Goto PrintG
    Lbl g17

    Print ""
    Print "Schnittwinkel:"
    20⇒θg
    angle(θven,θvfn)⇒θang
    Goto PrintAng
    Lbl g20

  IfEnd
  Goto Ende

 Case 5
  Print "Gerade - Gerade"
  Print ""

  18⇒θg
  θgAx⇒θAx : θgAy⇒θAy : θgAz⇒θAz
  θgBx⇒θBx : θgBy⇒θBy : θgBz⇒θBz  
  "g"⇒θname
  "λ"⇒θλ
  Goto PrintG
  Lbl g18
  Print ""

  14⇒θg
  θhAx⇒θAx : θhAy⇒θAy : θhAz⇒θAz
  θhBx⇒θBx : θhBy⇒θBy : θhBz⇒θBz 
  "h"⇒θname
  "κ"⇒θλ
  Goto PrintG
  Lbl g14
  Print ""

  θhBx/θgBx⇒θu1
  θhBy/θgBy⇒θu2
  θhBz/θgBz⇒θu3

  
  If θu1=θu2 And θu2=θu3 
  Then
    'Parallel
    (θgAx-θhAx)/θhBx⇒θv1
    (θgAy-θhAy)/θhBy⇒θv2
    (θgAz-θhAz)/θhBz⇒θv3
    If θv1=θv2 And θv2=θv3 
    Then
      Print "g = h"
      Print "Geraden g und h sind identisch"
    Else
      Print "g ‖ h"
      Print "Geraden g und h sind parallel"
    IfEnd  
  Else

    -(θhAx×hBy - θhBx×hAy + θhBx×gAy - θgAx×hBy) / (θhBx×gBy - θgBx×hBy) ⇒ θv1
    -(θhAy×hBz - θhBy×hAz + θhBy×gAz - θgAy×hBz) / (θhBy×gBz - θgBy×hBz) ⇒ θv2
    -(θhAz×hBx - θhBz×hAx + θhBz×gAx - θgAz×hBx) / (θhBz×gBx - θgBz×hBx) ⇒ θv3

    If θv1=θv2
    Then 
      θv1⇒θv    
    ElseIf θv2=θv3
    Then 
      θv2⇒θv    
    ElseIf θv3=θv1
    Then 
      θv3⇒θv
    IfEnd


    If θv1=θv2 Or θv2=θv3 Or θv3=θv1
    Then
      Print "g ⋂ h = S"
      Print "Geraden g und h schneiden sich im Punkt S"

      19⇒θg
      θgAx+θgBx×θv⇒θx
      θgAy+θgBy×θv⇒θy
      θgAz+θgBz×θv⇒θz
      "S"⇒θname
      Goto PrintP
      Lbl g19
  
      Print ""
      Print "Schnittwinkel:"
    Else
      Print "g ∦ h   ∧   g ⋂ h = Ø"
      Print "Geraden g und h sind windschief"
      Print ""
      Print "Ihr Winkel zu einander beträgt"
    IfEnd

    21⇒θg
    angle([θgBx,θgBy,θgBz],[θhBx,θhBy,θhBz])⇒θang
    Goto PrintAng
    Lbl g21

  IfEnd

  Goto Ende


 Case 6
  Print "Punkt - Punkt"
  Print ""

  10⇒θg
  "P"⇒θname
  θpx⇒θx:θpy⇒θy:θpz⇒θz
  Goto PrintP
  Lbl g10
  11⇒θg
  "Q"⇒name
  θqx⇒θx:θqy⇒θy:θqz⇒θz
  Goto PrintP
  Lbl g11

  If θpx=θx And θpy=θy And θpz=θz 
  Then
    Print "Punkte sind Identisch"
  Else
    Print "Abstand"
    norm([θqx-θpx,θqy-θpy,θqz-θpz])⇒θd
    ExpToStr θd,θt
    StrJoin " d = ",θt,θs
    Print θs
    
    NumToStr θd,θf,θt
    StrJoin "   ≈ ", θt, θs
    Print θs
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
  StrJoin " ", θname, θs
  StrJoin θs,"( ",θs 
  ExpToStr θx,θt
  StrJoin θs, θt, θs
  StrJoin θs, " | ", θs
  ExpToStr θy,θt
  StrJoin θs, θt, θs
  StrJoin θs, " | ", θs
  ExpToStr θz,θt
  StrJoin θs, θt, θs
  StrJoin θs, " )", θs
  Print θs

  StrJoin "≈", θname, θs
  StrJoin θs,"( ",θs 
  NumToStr θx,θf,θt
  StrJoin θs, θt, θs
  StrJoin θs, " | ", θs
  NumToStr θy,θf,θt
  StrJoin θs, θt, θs
  StrJoin θs, " | ", θs
  NumToStr θz,θf,θt
  StrJoin θs, θt, θs
  StrJoin θs, " )", θs
  Print θs
Goto gotog

Lbl PrintE
  StrJoin θname,": (",θs
  ExpToStr θa,θt
  StrJoin θs, θt, θs
  StrJoin θs, ")x + (", θs
  ExpToStr θb,θt
  StrJoin θs, θt, θs
  StrJoin θs, ")y + (", θs
  ExpToStr θc,θt
  StrJoin θs, θt, θs
  StrJoin θs, ")z = ", θs
  ExpToStr θD,θt
  StrJoin θs, θt, θs
  Print θs

  ""⇒θs
  StrLen θname, θt
  For 1⇒θi to θt
    StrJoin θs, " ", θs
  Next
  StrJoin θs," ≈",θs
  NumToStr θa,θf,θt
  StrJoin θs, θt, θs
  StrJoin θs, "x + ", θs
  NumToStr θb,θf,θt
  StrJoin θs, θt, θs
  StrJoin θs, "y + ", θs
  NumToStr θc,θf,θt
  StrJoin θs, θt, θs
  StrJoin θs, "z = ", θs
  NumToStr θD,θf,θt
  StrJoin θs, θt, θs
  Print θs
Goto gotog

Lbl PrintG
  ""⇒θs1:"‛"⇒θs2:""⇒θs3

  NumToStr θAx, θf, θu1
  NumToStr θAy, θf, θu2
  NumToStr θAz, θf, θu3
  StrLen θu1, θu1
  StrLen θu2, θu2
  StrLen θu3, θu3
  max(θu1, max(θu2, θu3)) ⇒ θu
  
  NumToStr θAx, θf, θt
  StrJoin θs1,"       |",θs1
  StrJoin θs1, θt, θs1
  For 1⇒θi to θu-θu1
    StrJoin θs1, " ", θs1
  Next
  StrJoin θs1, "|    |", θs1

  StrJoin θs2, θname, θs2
  StrJoin θs2,": x ≈ |",θs2
  NumToStr θAy, θf, θt
  StrJoin θs2, θt, θs2
  For 1⇒θi to θu-θu2
    StrJoin θs2, " ", θs2
  Next
  StrJoin θs2, "| + λ|", θs2

  NumToStr θAz, θf, θt
  StrJoin θs3,"       |",θs3
  StrJoin θs3, θt, θs3
  For 1⇒θi to θu-θu3
    StrJoin θs3, " ", θs3
  Next
  StrJoin θs3, "|    |", θs3

  NumToStr θBx, θf, θu1
  NumToStr θBy, θf, θu2
  NumToStr θBz, θf, θu3
  StrLen θu1, θu1
  StrLen θu2, θu2
  StrLen θu3, θu3
  max(θu1, max(θu2, θu3)) ⇒ θu

  NumToStr θBx, θf, θt
  StrJoin θs1,θt,θs1
  For 1⇒θi to θu-θu1
    StrJoin θs1, " ", θs1
  Next
  StrJoin θs1, "|",θs1

  NumToStr θBy, θf, θt
  StrJoin θs2,θt,θs2
  For 1⇒θi to θu-θu2
    StrJoin θs2, " ", θs2
  Next
  StrJoin θs2, "|",θs2

  NumToStr θBz, θf, θt
  StrJoin θs3,θt,θs3
  For 1⇒θi to θu-θu3
    StrJoin θs3, " ", θs3
  Next
  StrJoin θs3, "|",θs3



  ExpToStr θAx, θu1
  ExpToStr θAy, θu2
  ExpToStr θAz, θu3
  StrLen θu1, θu1
  StrLen θu2, θu2
  StrLen θu3, θu3
  max(θu1, max(θu2, θu3)) ⇒ θu
  
  ExpToStr θAx, θt
  StrJoin θs1,"       |",θs1
  StrJoin θs1, θt, θs1
  For 1⇒θi to θu-θu1
    StrJoin θs1, " ", θs1
  Next
  StrJoin θs1, "|    |", θs1
  StrJoin θs2,"   x = |",θs2

  ExpToStr θAy, θt
  StrJoin θs2, θt, θs2
  For 1⇒θi to θu-θu2
    StrJoin θs2, " ", θs2
  Next
  StrJoin θs2, "| + ", θs2
  StrJoin θs2, θλ, θs2
  StrJoin θs2, "|", θs2


  ExpToStr θAz, θt
  StrJoin θs3,"       |",θs3
  StrJoin θs3, θt, θs3
  For 1⇒θi to θu-θu3
    StrJoin θs3, " ", θs3
  Next
  StrJoin θs3, "|    |", θs3

  ExpToStr θBx, θu1
  ExpToStr θBy, θu2
  ExpToStr θBz, θu3
  StrLen θu1, θu1
  StrLen θu2, θu2
  StrLen θu3, θu3
  max(θu1, max(θu2, θu3)) ⇒ θu

  ExpToStr θBx, θt
  StrJoin θs1,θt,θs1
  For 1⇒θi to θu-θu1
    StrJoin θs1, " ", θs1
  Next
  StrJoin θs1, "|",θs1

  ExpToStr θBy, θt
  StrJoin θs2,θt,θs2
  For 1⇒θi to θu-θu2
    StrJoin θs2, " ", θs2
  Next
  StrJoin θs2, "|",θs2

  ExpToStr θBz, θt
  StrJoin θs3,θt,θs3
  For 1⇒θi to θu-θu3
    StrJoin θs3, " ", θs3
  Next
  StrJoin θs3, "|",θs3


  Print θs1
  Print θs2
  Print θs3
Goto gotog


Lbl PrintAng
  ExpToStr θang,θs
  StrJoin "∠ = (",θs,θs
  StrJoin θs,")°",θs
  Print θs
  NumToStr θang,θf,θs
  StrJoin "  ≈ ",θs,θs
  StrJoin θs,"°",θs
  Print θs
Goto gotog


'Input Methoden
Lbl InputP
  ClrText
  Print ""

  StrJoin "Punkt ", θname, θs
  StrJoin θs, " = ( x | y | z )", θs
  Print θs
  Input θx, "x" 
  Input θy, "y"
  Input θz, "z"
Goto gotog

Lbl InputG
  ClrText
  StrJoin "Gerade ", θname, θs
  StrJoin θs, ":", θs
  Print θs
  Print "    |Ax|   |Bx|"
  Print "x = |Ay| + |By|"
  Print "    |Az|   |Bz|"
  Input θAx, "Ax"
  Input θAy, "Ay"
  Input θAz, "Az"
  Input θBx, "Bx"
  Input θBy, "By"
  Input θBz, "Bz"
Goto gotog

Lbl InputE
  ClrText
  StrJoin "Ebene ",θname,θs
  StrJoin θs,": ax + by + cz = D",θs
  Print θs
  Input θa, "a" 
  Input θb, "b"
  Input θc, "c"
  Input θD, "D"
Goto gotog


'Return to Label
Lbl gotog
Switch θg
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
 Case 20:Goto g20
 Case 21:Goto g21
 Case 22:Goto g22
 Default
  Goto Ende
SwitchEnd


Lbl Ende

