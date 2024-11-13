from itertools import permutations

def find_position(N, K, placement):
    
    all_permutations = sorted(permutations(range(1, N + 1), K))
    position = all_permutations.index(tuple(placement)) + 1
    return position

with open("INPUT.TXT", "r") as infile:
    N, K = map(int, infile.readline().split())
    placement = list(map(int, infile.readline().split()))

result = find_position(N, K, placement)
with open("OUTPUT.TXT", "w") as outfile:
    outfile.write(str(result))
