package Task;

import java.rmi.Remote;

public interface IComputeTask extends Remote {
    TaskResult compute(Task task);
}
