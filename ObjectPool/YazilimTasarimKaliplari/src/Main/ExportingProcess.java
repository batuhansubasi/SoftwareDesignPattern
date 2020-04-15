package Main;

public class ExportingProcess {  
 private long processNo;  
  
    public ExportingProcess(long processNo)  {  
         this.processNo = processNo;    
		 
      System.out.println("İsleme numarası olan nesne " + processNo + " yaratildi.");  
     }  
     
    public long getProcessNo() {  
        return processNo;  
    }  
}
