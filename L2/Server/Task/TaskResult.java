package Task;

import java.io.Serializable;

public class TaskResult implements Serializable {
    private final String message;
    private final int firstPartOfResult;
    private final int secondPartOfResult;

    public String operation;

    public TaskResult(String message, int firstPartOfResult, int secondPartOfResult) {
        this.message = message;
        this.firstPartOfResult = firstPartOfResult;
        this.secondPartOfResult = secondPartOfResult;
    }

    public String getMessage() {
        return message;
    }

    public int getFirstPartOfResult() {
        return firstPartOfResult;
    }

    public int getSecondPartOfResult() {
        return secondPartOfResult;
    }
}
