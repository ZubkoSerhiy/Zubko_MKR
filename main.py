def cyclic_distance(char1, char2):

    dist = abs(ord(char1) - ord(char2))
    return min(dist, 26 - dist)

def total_cyclic_distance(sa, sb):

    return sum(cyclic_distance(sa[i], sb[i]) for i in range(len(sa)))

def main():

    with open("INPUT.TXT", "r") as f:
        lines = f.read().splitlines()
    
    sa, sb = lines[0], lines[1]

    result = total_cyclic_distance(sa, sb)
    with open("OUTPUT.TXT", "w") as f:
        f.write(str(result))

if __name__ == "__main__":
    main()
