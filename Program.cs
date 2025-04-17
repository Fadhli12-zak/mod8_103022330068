using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class BankTransferConfig
{
    public string lang { get; set; } 
    public Transfer transfer { get; set; }
    public string[] method { get; set; }
    public Confirmation confirmation { get; set; }
    private const string FilePath = "bank_transfer_config.json";
    
    public static BankTransferConfig LoadFromFile()
    {
            string json = File.ReadAllText(FilePath);
            var config = JsonSerializer.Deserialize<BankTransferConfig>(json);
            return config;
    }
   
    public void WriteNewConfig()
    {
        string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }
}
public class Transfer
{
    public int threshold { get; set; }
    public int low_fee { get; set; }
    public int high_fee { get; set; }

    public Transfer(int threshold, int low_fee, int high_fee)
    {
        this.threshold = threshold;
        this.low_fee = low_fee;
        this.high_fee = high_fee;
    }
}
public class Confirmation
{
    public string en { get; set; }
    public string id { get; set; }

    public Confirmation(string en, string id)
    {
        this.en = en;
        this.id = id;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BankTransferConfig config = new BankTransferConfig();
        if (config.lang == "en")
        {
            Console.WriteLine("Please insert the amount of money to transfer ");
        }
        else if (config.lang == "id")
        {
            Console.WriteLine("Masukkan jumlah uang yang akan ditransfer ");
        }
        int total = 0;
        int amount = Convert.ToInt32(Console.ReadLine());
        if (amount <= config.transfer.threshold)
        {
            total = amount + config.transfer.low_fee;
        }
        else
        {
            total = amount + config.transfer.high_fee;
        }

        if (config.lang == "en")
        {
            Console.WriteLine($"transfer fee = {amount}");
            Console.WriteLine($"Total Biaya={total + amount}");
        }
        else if (config.lang == "id")
        {
            Console.WriteLine($"Biaya transfer = {amount}");
            Console.WriteLine($"Total Biaya={total + amount}");
        }

        if (config.lang == "en")
        {
            Console.WriteLine("Select transfer method");
        }
        else if (config.lang == "id")
        {
            Console.WriteLine("Pilih metode transfer");
        }
       
    }
}