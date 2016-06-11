package com.compareexample;


import java.util.Comparator;

public class stringComparator implements Comparator<String> {


    public int compare(String str1, String  str2) {
        return  Integer.compare(str1.length(), str2.length());

    }
}
