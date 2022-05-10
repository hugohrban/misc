class Prvek:
    def __init__(self, x, dalsi):
        self.x = x
        self.dalsi = dalsi

def VytiskniLSS( p ):
    print( "LSS:", end=" " )
    while p!=None:
        print( p.x, end=" " )
        p = p.dalsi
    print(".")

def NactiLSS():
    """cte cisla z radku, dokud nenacte prazdny radek"""
    prvni = None
    posledni = None
    r = input()
    while r!="":
        radek = r.split()
        if len(radek)==0: # protoze ten test r!="" v RCDX neukoncil cyklus!
            break
        for s in radek:
            p = Prvek(int(s),None)
            if prvni==None:
                prvni = p
            else:
                posledni.dalsi = p
            posledni = p
        r = input()
    return prvni

#################################################

def UnionDestruct(a,b):
    """ destruktivni prunik dvou usporadanych seznamu
    * nevytvari zadne nove prvky, vysledny seznam bude poskladany z prvku puvodnich seznamu,
    * vysledek je MNOZINA, takze se hodnoty neopakuji """
    if a == None:
        return b
    if b == None:
        return a

    zac = None

    while a != None and b != None:        
        if a.x < b.x:
            if zac == None:
                zac = a
                kon = a
            else:
                kon.dalsi = a
                kon = kon.dalsi
            a = a.dalsi
        elif a.x > b.x:
            if zac == None:
                zac = b
                kon = b
            else:
                kon.dalsi = b
                kon = kon.dalsi
            b = b.dalsi
        else:
            if zac == None:
                zac = a
                kon = a
            else:
                kon.dalsi = a
                kon = kon.dalsi
            a = a.dalsi
            b = b.dalsi

        if a == None:
            while b != None:
                kon.dalsi = b
                kon = kon.dalsi
                b = b.dalsi
        if b == None:
            while a != None:
                kon.dalsi = a
                kon = kon.dalsi
                a = a.dalsi
        
    return zac

#################################################

VytiskniLSS( UnionDestruct( NactiLSS(), NactiLSS() ) )
