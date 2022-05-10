class Stav():
    def __init__(self, a, b, c):
        self.x = [a,b,c]
        self.min_pocet = None
        self.dalsi = None

class Fronta():
    def __init__(self):
        self.start = Stav(None, None, None)
        self.koniec = None
    def enq(self, stav):
        if self.koniec == None:
            self.start.dalsi = stav
            self.koniec = stav
        else:
            self.koniec.dalsi = stav
            self.koniec = stav
    def deq(self):
        preskumane_stavy.append(self.start.dalsi.x)
        if self.start.dalsi == None:
            print("prázdna fronta")
            return
        elif self.start.dalsi.dalsi == None:
            self.start.dalsi = None
            self.koniec = self.start.dalsi
        else:
            self.start.dalsi = self.start.dalsi.dalsi
            p = self.start
            while p.dalsi != None:
                p = p.dalsi
            self.koniec = p

def JeVoFronte(fronta, stav):
    p = fronta.start.dalsi
    while p != None:
        if p.x == stav.x:
            return True
        p = p.dalsi
    
def JeToDovolene(stav, odkial, kam):
    if stav.x[odkial] == 0 or stav.x[kam] == max_objemy[kam]:
        return False
    else:
        return True

def NovyStav(povodny_stav, odkial, kam):
    novy_stav = Stav(povodny_stav.x[0], povodny_stav.x[1], povodny_stav.x[2])
    a = novy_stav.x[odkial]
    b = novy_stav.x[kam]
    while a > 0 and b < max_objemy[kam]:
        a-=1
        b+=1
    novy_stav.x[odkial] = a
    novy_stav.x[kam] = b
    novy_stav.min_pocet = povodny_stav.min_pocet+1
    return novy_stav


def prehladavanie(stav):
    for i in range(3):
        if JeToDovolene(stav, i%3, (i+1)%3):
            novy = NovyStav(stav, i%3, (i+1)%3)
            for objem in novy.x:
                if odpovede[objem] == None or novy.min_pocet < odpovede[objem]:
                    odpovede[objem] = novy.min_pocet
            if novy.x not in preskumane_stavy and not JeVoFronte(f, novy):
                f.enq(novy)
        if JeToDovolene(stav, i%3, (i-1)%3):
            novy = NovyStav(stav, i%3, (i-1)%3)
            for objem in novy.x:
                if odpovede[objem] == None or novy.min_pocet < odpovede[objem]:
                    odpovede[objem] = novy.min_pocet
            if novy.x not in preskumane_stavy and not JeVoFronte(f, novy):
                f.enq(novy)

a, b, c, x, y, z = map(int, input().split())

max_objemy = [a, b, c]
preskumane_stavy = []
f = Fronta()
odpovede = [None]*(a+b+c)

s1 = Stav(x, y, z)
s1.min_pocet = 0
f.enq(s1)

for i in s1.x:
    odpovede[i] = 0

while f.start.dalsi != None:
    prehladavanie(f.start.dalsi)
    f.deq()

for i in range(len(odpovede)):
    if odpovede[i] != None:
        print(f"{i}:{odpovede[i]}", end=" ")
