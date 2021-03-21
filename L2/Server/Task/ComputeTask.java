package Task;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;

public class ComputeTask extends UnicastRemoteObject implements IComputeTask {

    public ComputeTask() throws RemoteException {
        super();
    }

    @Override
    public TaskResult compute(Task task) throws RemoteException {
        TaskResult result;
        List<Integer> numbers = new ArrayList<>();
        for (Integer i : task.getNumbers()) {
            numbers.add(i);
        }
        switch (task.operation) {
            case "nearestAvg":
                result = getNearestValue(numbers, task.getNearestValue());
                break;
            case "minMax":
                result = getMinMaxValues(numbers);
                break;
            default:
                result = new TaskResult("ERROR 404 - operation not found",
                        0, 0);
        }
        result.operation = task.operation;
        return result;
    }

    private TaskResult getNearestValue(List<Integer> numbers, int value) {
        int searchedValue = -1;
        int deviation = -1;

        for (Integer number : numbers) {
            int numberDeviation = Math.abs(number - value);
            if (deviation == -1) {
                searchedValue = number;
                deviation = numberDeviation;
            } else if (numberDeviation < deviation) {
                searchedValue = number;
                deviation = numberDeviation;
            }
        }

        String message;
        if (deviation != -1) {
            message = "Found nearest value";
        } else {
            message = "List is empty";
        }

        return new TaskResult(message, searchedValue, value);
    }

    private TaskResult getMinMaxValues(List<Integer> numbers) {
        TaskResult result;
        if (numbers.size() > 0) {
            int min = numbers.get(0);
            int max = numbers.get(0);
            for (Integer number : numbers) {
                if (number < min) {
                    min = number;
                } else if (number > max) {
                    max = number;
                }
            }
            result = new TaskResult("Found min & max values", min, max);
        } else {
            result = new TaskResult("Not found min & max values",
                    0, 0);
        }

        return result;
    }
}
