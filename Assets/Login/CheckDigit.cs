public class CheckDigit
{
   public bool IsCorrectCheckDigit(string subjectNumberString)
    {
        int DigitsSum = 0;

        try
        {
             for (int i = 0; i < subjectNumberString.Length; i++)
                    {
                        int add = 0;
                        if(i % 2 == 1)
                        {
                            add = int.Parse(subjectNumberString[i].ToString()) * 3;
                        }
                        else
                        {
                            add = int.Parse(subjectNumberString[i].ToString());
                        }
                        DigitsSum += add; 
                    }
        }
        catch
        {
            return false;
        }

        if (DigitsSum % 10 == 0)
        {
            return true;
        }
        return false;
    }
}
