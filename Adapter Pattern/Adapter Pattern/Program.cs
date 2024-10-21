using System;

public class ComputerGame
{
    private string name;
    private PegiAgeRating pegiAgeRating;
    private double budgetInMillionsOfDollars;
    private int minimumGpuMemoryInMegabytes;
    private int diskSpaceNeededInGB;
    private int ramNeededInGb;
    private int coresNeeded;
    private double coreSpeedInGhz;

    public ComputerGame(string name,
                        PegiAgeRating pegiAgeRating,
                        double budgetInMillionsOfDollars,
                        int minimumGpuMemoryInMegabytes,
                        int diskSpaceNeededInGB,
                        int ramNeededInGb,
                        int coresNeeded,
                        double coreSpeedInGhz)
    {
        this.name = name;
        this.pegiAgeRating = pegiAgeRating;
        this.budgetInMillionsOfDollars = budgetInMillionsOfDollars;
        this.minimumGpuMemoryInMegabytes = minimumGpuMemoryInMegabytes;
        this.diskSpaceNeededInGB = diskSpaceNeededInGB;
        this.ramNeededInGb = ramNeededInGb;
        this.coresNeeded = coresNeeded;
        this.coreSpeedInGhz = coreSpeedInGhz;
    }

    public string getName()
    {
        return name;
    }

    public PegiAgeRating getPegiAgeRating()
    {
        return pegiAgeRating;
    }

    public double getBudgetInMillionsOfDollars()
    {
        return budgetInMillionsOfDollars;
    }

    public int getMinimumGpuMemoryInMegabytes()
    {
        return minimumGpuMemoryInMegabytes;
    }

    public int getDiskSpaceNeededInGB()
    {
        return diskSpaceNeededInGB;
    }

    public int getRamNeededInGb()
    {
        return ramNeededInGb;
    }

    public int getCoresNeeded()
    {
        return coresNeeded;
    }

    public double getCoreSpeedInGhz()
    {
        return coreSpeedInGhz;
    }
}

public enum PegiAgeRating
{
    P3, P7, P12, P16, P18
}

public class Requirements
{
    private int gpuGb;
    private int HDDGb;
    private int RAMGb;
    private double cpuGhz;
    private int coresNum;

    public Requirements(int gpuGb,
                        int HDDGb,
                        int RAMGb,
                        double cpuGhz,
                        int coresNum)
    {
        this.gpuGb = gpuGb;
        this.HDDGb = HDDGb;
        this.RAMGb = RAMGb;
        this.cpuGhz = cpuGhz;
        this.coresNum = coresNum;
    }

    public int getGpuGb()
    {
        return gpuGb;
    }

    public int getHDDGb()
    {
        return HDDGb;
    }

    public int getRAMGb()
    {
        return RAMGb;
    }

    public double getCpuGhz()
    {
        return cpuGhz;
    }

    public int getCoresNum()
    {
        return coresNum;
    }
}

public interface PCGame
{
    string getTitle();
    int getPegiAllowedAge();
    bool isTripleAGame();
    Requirements getRequirements();
}

public class ComputerGameAdapter : PCGame
{
    private ComputerGame computerGame;

    public ComputerGameAdapter(ComputerGame computerGame)
    {
        this.computerGame = computerGame;
    }

    public string getTitle()
    {
        return computerGame.getName();
    }

    public int getPegiAllowedAge()
    {
        switch (computerGame.getPegiAgeRating())
        {
            case PegiAgeRating.P3: return 3;
            case PegiAgeRating.P7: return 7;
            case PegiAgeRating.P12: return 12;
            case PegiAgeRating.P16: return 16;
            case PegiAgeRating.P18: return 18;
            default: return 0;
        }
    }

    public bool isTripleAGame()
    {
        return computerGame.getBudgetInMillionsOfDollars() > 50;
    }

    public Requirements getRequirements()
    {
        return new Requirements(
            computerGame.getMinimumGpuMemoryInMegabytes() / 1024,
            computerGame.getDiskSpaceNeededInGB(),
            computerGame.getRamNeededInGb(),
            computerGame.getCoreSpeedInGhz(),
            computerGame.getCoresNeeded()
        );
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Пример создания игры
        ComputerGame game = new ComputerGame(
            "Epic Adventure",
            PegiAgeRating.P16,
            60, // Бюджет в миллионах
            4096, // GPU память в мегабайтах
            50, // Дисковое пространство в ГБ
            16, // ОЗУ в ГБ
            4, // Ядра
            3.5 // Частота в ГГц
        );

        // Создание адаптера
        PCGame gameAdapter = new ComputerGameAdapter(game);

        // Использование адаптера
        Console.WriteLine($"Title: {gameAdapter.getTitle()}");
        Console.WriteLine($"PEGI Allowed Age: {gameAdapter.getPegiAllowedAge()}");
        Console.WriteLine($"Is Triple A Game: {gameAdapter.isTripleAGame()}");
        Requirements reqs = gameAdapter.getRequirements();
        Console.WriteLine($"GPU Memory: {reqs.getGpuGb() / 8.0} Gb"); // GPU Memory in gigabits
        Console.WriteLine($"Disk Space: {reqs.getHDDGb() / 8.0} Gb"); // Disk space in gigabits
        Console.WriteLine($"RAM: {reqs.getRAMGb() / 8.0} Gb"); // RAM in gigabits
        Console.WriteLine($"CPU Speed: {reqs.getCpuGhz()} GHz"); // CPU speed in GHz
        Console.WriteLine($"Cores: {reqs.getCoresNum()}"); // Number of cores
    }
}
