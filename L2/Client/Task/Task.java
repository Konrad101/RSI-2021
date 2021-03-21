package Task;

import java.io.Serializable;

public class Task implements Serializable {
    private static final long SERIAL_VERSION_UID = 103L;

    // szukaj liczby najblizej sredniej, max min
    public String operation;
    private final int[] numbers;
    private int nearestValue;

    public int[] getNumbers() {
        return numbers;
    }

    public Task(int[] numbers){
        this.numbers = numbers;
    }

    public int getNearestValue() {
        return nearestValue;
    }

    public void setNearestValue(int nearestValue) {
        this.nearestValue = nearestValue;
    }
}
