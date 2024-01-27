package main

import (
	crand "crypto/rand"
	"fmt"
	"math/rand"
)

func canCompleteCircuit(gas []int, cost []int) int {
	n := len(gas)
	if n == 0 || len(cost) != n {
		return -1
	}

	// find first candidate
	cur := gas[n-1] - cost[n-1]
	bal := cur
	var candidate, i, lastBal int
	if bal > 0 {
		candidate = n - 1

		for i = n - 2; i >= 0; i-- {
			cur = gas[i] - cost[i]
			if cur > 0 {
				bal += cur
			} else {
				break
			}
		}

		if i == -1 {
			return candidate
		}

		candidate = i + 1
		lastBal = 0
	} else {
		i = n - 2
		candidate = -1
		lastBal = -1
	}

	// main cycle: [left, right)
	left, right := i+1, 0
	for left > right {
		var next int
		if bal > 0 {
			if lastBal <= 0 {
				candidate = left

				// went from negative to positive, be greedy now
				for ; left > right; left-- {
					next = left - 1
					cur = gas[next] - cost[next]

					if cur > 0 {
						bal += cur
						candidate = next
					} else {
						break
					}
				}
			}

			// keep extending right while balance is positive
			next = right
			right++
		} else {
			if lastBal > 0 {
				// went from positive to negative, be greedy now
				for ; left > right; right++ {
					cur = gas[right] - cost[right]

					if cur <= 0 {
						bal += cur
					} else {
						break
					}
				}
			}

			// keep extending left while balance is negative
			left--
			next = left
		}

		lastBal = bal
		bal += gas[next] - cost[next]
	}

	if bal >= 0 {
		if candidate == -1 || lastBal < 0 {
			candidate = left
		}
		return candidate
	} else {
		return -1
	}
}

func runTest(gas []int, cost []int, expected ...int) {
	actual := canCompleteCircuit(gas, cost)

	fmt.Printf("Gas: %v\n", gas)
	fmt.Printf("Cost: %v\n", cost)
	fmt.Println("=>")
	fmt.Println(actual)

	if expected == nil {
		return
	}

	for _, v := range expected {
		if v == actual {
			return
		}
	}

	err := fmt.Sprintf("Expected %d, got %d", expected, actual)
	panic(err)
}

func randomArray(n int) []int {
	bytes := make([]byte, n)
	_, _ = crand.Read(bytes)
	ints := make([]int, n)
	for i, v := range bytes {
		ints[i] = int(v)
	}

	return ints
}

func main() {
	// original #1
	runTest([]int{1, 2, 3, 4, 5}, []int{3, 4, 5, 1, 2}, 3)

	// original #2
	runTest([]int{2, 3, 4}, []int{3, 4, 3}, -1)

	// tricky leetcode #1
	runTest([]int{3, 1, 1}, []int{1, 2, 2}, 0)

	// tricky leetcode #2
	runTest([]int{6, 1, 4, 3, 5}, []int{3, 8, 2, 4, 2}, 2)

	// tricky leetcode #3
	runTest([]int{2, 0, 0}, []int{0, 1, 0}, 0)

	// tricky random
	runTest(
		[]int{6, 77, 175, 32, 252, 146, 210, 50, 194},
		[]int{159, 117, 136, 62, 91, 176, 159, 225, 4},
		2, 4)

	// random
	n := rand.Intn(20) + 1
	runTest(randomArray(n), randomArray(n))
}
