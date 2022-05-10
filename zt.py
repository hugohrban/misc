den, mesiac, rok = map(int, input().split())
o_kolko_dni = int(input())

m31 = [1, 3, 5, 7, 8, 10, 12]
m30 = [4, 6, 9, 11]


def je_prestupny(rok):
    """zisti ci je rok prestupny"""

    if rok%4==0:
        if rok%100==0:
            if rok%400 == 0:
                return True
            return False
        return True
    return False


def je_posledny(d, m, r):
    """ziti ci je to posledny den v mesiaci"""
    
    if m in m31 and d==31:
        return True
    elif m in m30 and d==30:
        return True
    elif m==2 and d==28 and not je_prestupny(r):
        return True
    elif m==2 and d==29 and je_prestupny(r):
        return True
    return False


def pridaj_rok(d, m, r):
    global o_kolko_dni
    if (je_prestupny(r) and m<3) or (je_prestupny(r+1) and m>2):
        o_kolko_dni -= 366
        return d, m, r+1
    else:
        o_kolko_dni -= 365
        return d, m, r+1


def pridaj_den(d, m, r):
    global o_kolko_dni
    o_kolko_dni -= 1
    if d==31 and m==12:
        return 1, 1, r+1
    elif je_posledny(d, m, r):
        return 1, m+1, r
    else:
        return d+1, m, r
    

vys = (den, mesiac, rok)

if den==29 and mesiac==2 and je_prestupny(rok):
    vys = pridaj_den(vys[0], vys[1], vys[2])


while o_kolko_dni > 366:
    vys = pridaj_rok(vys[0], vys[1], vys[2])

while o_kolko_dni > 0:
    vys = pridaj_den(vys[0], vys[1], vys[2])

for cislo in vys:
    print(cislo, end=" ")

