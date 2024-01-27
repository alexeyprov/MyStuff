package main

import (
	"fmt"
	"math/rand"
)

func trap(height []int) int {
	if height == nil || len(height) < 3 {
		return 0
	}

	total := 0
	subTotal := 0
	edge := 0
	nextEdge := 0
	last := 0

	var lineStarts []int

	for idx, current := range height {

		if current < last && current >= 0 {
			// dropping

			if nextEdge > edge {
				edge = nextEdge
				lineStarts = make([]int, edge)
				total += subTotal
				subTotal = 0
			}

			// (re-)set lineStarts
			for i := current + 1; i <= last; i++ {
				lineStarts[i-1] = idx
			}
		} else if current > last {
			// climbing

			var maxFill int
			if current > edge {
				maxFill = max(edge, last)
				nextEdge = current
			} else {
				maxFill = current
			}

			// add everything <= maxFill
			for i := last + 1; i <= maxFill; i++ {
				subTotal += idx - lineStarts[i-1]
			}
		}

		last = current
	}

	return total + subTotal
}

func runTest(a []int, expected int) {
	result := trap(a)
	//s, _ := json.Marshal(a)
	fmt.Printf("%v -> %d\n", a, result)
	if expected >= 0 && result != expected {
		err := fmt.Sprintf("Expected %d, Got %d", expected, result)
		panic(err)
	}
}

func main() {
	// original #1
	runTest([]int{0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1}, 6)

	// original #2
	runTest([]int{4, 2, 0, 3, 2, 5}, 9)

	// tricky
	runTest([]int{10, 7, 6, 5, 6, 5, 7, 6, 7, 8, 7, 7, 9, 9, 10}, 41)

	// random
	dim := rand.Intn(15) + 1
	runTest(rand.Perm(dim), -1)
}
