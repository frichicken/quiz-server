public static class Utilities
{
    public static string GetRandomString(int size = 8)
    {
        Random random = new();
        string alphanumeric = "abcdefghijklmnopqrstuvwxyz0123456789";
        string result = "";

        for (int i = 0; i < size; i++)
        {
            int position = random.Next(alphanumeric.Length);
            result += alphanumeric[position];
        }

        return result;
    }
}