package com.compareexample;


import java.util.Arrays;
import java.util.Comparator;

public class Main {

    public static void main(String[] args) {
        Integer [] input = {2, 5, 34, 7, 13, 25, 9, 10, 16};
        String [] stringInput = {"qe", "qwe","asd","t"};
        Main main = new Main();

//        main.bubbleSort(input, new IntComparator());
        main.bubbleSort(stringInput, new stringComparator());

        System.out.println(Arrays.toString(stringInput));

    }

    public <T> void bubbleSort(T [] arr, Comparator<T> comparator) {
        boolean swapped = true;
        int j = 0;
        T tmp;
        while (swapped) {
            swapped = false;
            j++;
            for (int i = 0; i < arr.length - j; i++) {
                if (comparator.compare(arr[i], arr[i + 1]) > 0 ) {
                    tmp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = tmp;
                    swapped = true;
                }
            }
        }

    }
}
