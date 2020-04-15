package Main;

	public class ExportingTask implements Runnable {  
        private ObjectPool<ExportingProcess> pool;  
        private int threadNo;  
        public ExportingTask(ObjectPool<ExportingProcess> pool, int threadNo){  
            this.pool = pool;  
            this.threadNo = threadNo;  
        }  
      
        public void run() {  
            ExportingProcess exportingProcess = pool.borrowObject();  
            System.out.println("Thread " + threadNo + ": İsleme numarası olan nesne. "  
                    + exportingProcess.getProcessNo() + "alındı.");  
 
            pool.returnObject(exportingProcess);  
  
            System.out.println("Thread " + threadNo +": İsleme numarası olan nesne "  
                   + exportingProcess.getProcessNo() + " geri döndürüldü.");  
        }  
 
    }
