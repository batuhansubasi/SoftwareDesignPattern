package Main;

import java.util.concurrent.ExecutorService;  
import java.util.concurrent.Executors;  
import java.util.concurrent.TimeUnit;  
import java.util.concurrent.atomic.AtomicLong;
import java.util.Scanner;

public class Main{  
      private ObjectPool<ExportingProcess> pool;  
      private AtomicLong processNo=new AtomicLong(0);  
      public void setUp(int min, int max, int sure) {  
      pool = new ObjectPool<ExportingProcess>(min, max, sure)  
        {  
            protected ExportingProcess createObject()  
            {   
                return new ExportingProcess( processNo.incrementAndGet());  
            }  
        };  
    }  

    public void tearDown() {  
        pool.shutdown();  
    } 

    public void testObjectPool() {  
        ExecutorService executor = Executors.newFixedThreadPool(8);  

        executor.execute(new ExportingTask(pool, 1));  
        executor.execute(new ExportingTask(pool, 2));  
        executor.execute(new ExportingTask(pool, 3));  
        executor.execute(new ExportingTask(pool, 4));  
        executor.execute(new ExportingTask(pool, 5));  
        executor.execute(new ExportingTask(pool, 6));  
        executor.execute(new ExportingTask(pool, 7));  
        executor.execute(new ExportingTask(pool, 8));  
  
        executor.shutdown();  
        try {  
            executor.awaitTermination(30, TimeUnit.SECONDS);  
            } catch (InterruptedException e)  
              
              {  
               e.printStackTrace();  
              }  
    }  

    public static void main(String args[])  {   
        Main op=new Main();  
        
        Scanner scanIn = new Scanner(System.in);

        System.out.println("\nHavuz için minimum sayi giriniz");
        String sWhatever = scanIn.nextLine();
        int havuzMinimum=Integer.parseInt(sWhatever);

        sWhatever = "";

        System.out.println("\nHavuz için maximum sayi giriniz");
        sWhatever = scanIn.nextLine();
        int havuzMaximum=Integer.parseInt(sWhatever);

        sWhatever = "";

        System.out.println("\nSaniye bilgisi giriniz");
        sWhatever = scanIn.nextLine();
        int saniye=Integer.parseInt(sWhatever);

        scanIn.close();

        op.setUp(havuzMinimum,havuzMaximum,saniye);  
        op.tearDown();  
        op.testObjectPool();  
   }   
}