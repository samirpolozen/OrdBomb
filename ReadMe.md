# Letter Duel - Spelregler

## Beskrivning

Letter Duel är ett nätverksbaserat turbaserat multiplayer-spel.

Två spelare spelar mot samma spel och delar samma game state.

Servern väljer slumpmässigt hemligt ord från en lista med ord.

Spelarna turas om att gissa bokstäver.

---

# Spelregler

## 1. Starta spelet

När ett nytt spel startas väljer servern ett hemligt ord.

Exempel:

```text
backend
```

---

## 2. Två spelare

Spelet har:

* Player 1
* Player 2

Player 1 börjar alltid.

---

## 3. Turordning

Spelarna turas om att gissa EN bokstav åt gången.

Efter varje gissning byts turen till nästa spelare.

---

## 4. Rätt bokstav

Om bokstaven finns i det hemliga ordet:

* spelaren får 1 poäng
* bokstaven sparas i listan över gissade bokstäver

---

## 5. Fel bokstav

Om bokstaven INTE finns i ordet:

* spelaren får ingen poäng
* turen går vidare till nästa spelare

---

## 6. Samma bokstav får inte användas igen

Om en bokstav redan finns, så ignoreras gissningen.

Ingen extra poäng delas ut.


---

## 7. Delat game state

Båda spelarna spelar i samma match och ser samma information.

---

## 8. Game over

När alla bokstäver i det hemliga ordet har gissat, avslutas spelet.

Spelaren med högst poäng vinner
