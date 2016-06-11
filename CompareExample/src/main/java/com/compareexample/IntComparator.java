package com.compareexample;


import java.util.Comparator;

public class IntComparator implements Comparator<Integer> {


    public int compare(Integer int1, Integer int2) {
        return  Integer.compare(int1, int2 );

    }
}
