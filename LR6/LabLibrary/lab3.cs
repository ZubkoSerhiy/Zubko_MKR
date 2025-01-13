namespace LabLibrary
{
    public static class Lab3
    {
        public static string Run(string[] lines)
        {
            int result = ProcessData(lines);
            return result.ToString();
        }

        private static int ProcessData(string[] input)
        {
            string[] firstLine = input[0].Split();
            if (firstLine.Length < 3)
            {
                throw new ArgumentException("Input line does not contain enough values.");
            }

            int h = int.Parse(firstLine[0]);
            int m = int.Parse(firstLine[1]);
            int n = int.Parse(firstLine[2]);

            if (h <= 0 || m <= 0 || n <= 0)
            {
                throw new ArgumentException("Dimensions must be positive integers.");
            }

            if (input.Length < h * m + 1)
            {
                throw new ArgumentException("Input does not contain enough rows for the specified dimensions.");
            }

            char[,,] labyrinth = new char[h, m, n];
            (int x, int y, int z) start = (-1, -1, -1);
            (int x, int y, int z) end = (-1, -1, -1);

            int lineIndex = 1;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < m; j++, lineIndex++)
                {
                    if (lineIndex >= input.Length)
                    {
                        throw new IndexOutOfRangeException("Line index exceeds input length.");
                    }

                    string row = input[lineIndex].Trim();
                    if (row.Length != n)
                    {
                        throw new ArgumentException($"Row length {row.Length} does not match expected length {n}.");
                    }

                    for (int k = 0; k < n; k++)
                    {
                        labyrinth[i, j, k] = row[k];
                        if (row[k] == '1') start = (i, j, k);
                        if (row[k] == '2') end = (i, j, k);
                    }
                }
            }

            if (start == (-1, -1, -1) || end == (-1, -1, -1))
            {
                return -1;
            }

            int[,,] distances = new int[h, m, n];
            for (int i = 0; i < h; i++)
                for (int j = 0; j < m; j++)
                    for (int k = 0; k < n; k++)
                        distances[i, j, k] = int.MaxValue;

            Queue<(int x, int y, int z)> queue = new Queue<(int x, int y, int z)>();
            queue.Enqueue(start);
            distances[start.x, start.y, start.z] = 0;

            int[] dz = { 0, 0, 0, 0, 1, -1 };
            int[] dx = { -1, 1, 0, 0, 0, 0 };
            int[] dy = { 0, 0, -1, 1, 0, 0 };

            while (queue.Count > 0)
            {
                var (x, y, z) = queue.Dequeue();
                int currentDistance = distances[x, y, z];

                for (int d = 0; d < 6; d++)
                {
                    int nx = x + dx[d];
                    int ny = y + dy[d];
                    int nz = z + dz[d];

                    if (nx >= 0 && nx < h && ny >= 0 && ny < m && nz >= 0 && nz < n &&
                        labyrinth[nx, ny, nz] != 'o' && distances[nx, ny, nz] == int.MaxValue)
                    {
                        distances[nx, ny, nz] = currentDistance + 5;
                        queue.Enqueue((nx, ny, nz));

                        if ((nx, ny, nz) == end)
                            return distances[nx, ny, nz];
                    }
                }
            }

            return distances[end.x, end.y, end.z] == int.MaxValue ? -1 : distances[end.x, end.y, end.z];
        }
    }
}