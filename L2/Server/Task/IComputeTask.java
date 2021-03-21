package Task;

import java.rmi.Remote;
import java.rmi.RemoteException;

public interface IComputeTask extends Remote {
    TaskResult compute(Task task) throws RemoteException;
}
