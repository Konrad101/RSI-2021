import java.rmi.RemoteException;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;

public class MyServer {
    public static void main(String[] args) {
        System.setProperty("java.rmi.server.hostname","25.43.220.53");
        if (args.length == 0){
            System.out.println("You have to enter RMI object address in the form: //host_address/service_name");
            return;
        }

        if (System.getSecurityManager() == null)
            System.setSecurityManager(new SecurityManager());

        try {
            LocateRegistry.createRegistry(1099);
        } catch (RemoteException e) {
            e.printStackTrace();
        }

        try {
            CalcObject calcObj = new CalcObjImpl();
            java.rmi.Naming.rebind(args[0], calcObj);
            CalcObject2 calcObj2 = new CalcObjImpl2();
            java.rmi.Naming.rebind(args[1], calcObj2);
            System.out.println("Server is registered now :-)");
            System.out.println("Press Ctrl+C to stop...");
        } catch (Exception e) {
            System.out.println("SERVER CAN'T BE REGISTERED!");
            e.printStackTrace();
        }
    }
}