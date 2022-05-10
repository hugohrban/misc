r, s = map(int, input().split())
pole = [[None for i in range(s)] for j in range(r)]

def JeRemiza(pole, znak, x, y):         # test ci nie je 5 rovnakych znakov pri sebe
    if x >= 4:
        if znak == pole[x-1][y] == pole[x-2][y] == pole[x-3][y] == pole[x-4][y]:
            return False
    if y >= 4:
        if znak == pole[x][y-1] == pole[x][y-2] == pole[x][y-3] == pole[x][y-4]:
            return False
    if x >= 4 and y >= 4:
        if znak == pole[x-1][y-1] == pole[x-2][y-2] == pole[x-3][y-3] == pole[x-4][y-4]:
            return False
    if y >= 4 and (r-x) >= 5:
        if znak == pole[x+1][y-1] == pole[x+2][y-2] == pole[x+3][y-3] == pole[x+4][y-4]:
            return False
    return True


def DalsiePolicko(x, y):        # presun na nove policko
    if x == s-1:
        return 0, y+1
    else:
        return x+1, y


def NovyStav(pole, znak, x, y):
    while pole[r-1][s-1] == None: # kym su neni vsetky polia vypisane
        while x < r and y < s:

            if JeRemiza(pole, "X", x, y):       #ak je mozne, tak doplni "X"
                nove = pole.copy()
                nove[x][y] = "X"
                NovyStav(pole, "X", DalsiePolicko(x, y)[0], DalsiePolicko(x, y)[1])

            if JeRemiza(pole, "O", x, y):       #ak je mozne, tak doplni "O"
                nove = pole.copy()
                nove[x][y] = "O"
                NovyStav(pole, "O", DalsiePolicko(x, y)[0], DalsiePolicko(x, y)[1])

            return          # ak sa neda doplnit ani jedno, vraciame sa spat
        x = 0
        y += 1

    for riadok in pole:     #vypisanie vysledku a ukoncenie programu
        for znak in riadok:
            print(znak, end=" ")
        print()

    exit()

NovyStav(pole, "", 0, 0) 