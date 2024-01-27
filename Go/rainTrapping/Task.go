package main

type Task struct {
	heights []int
	size    int
	idx     int
}

func newTask(heights []int) *Task {
	t := new(Task)
	t.heights = heights
	t.size = len(heights)
	return t
}

func (t *Task) IsComplete() bool {
	return t.idx >= t.size
}

func (t *Task) Current() int {
	return t.heights[t.idx]
}

func (t *Task) MoveNext() {
	t.idx++
}

func (t *Task) Solve() int {
	total := 0
	subTotal := 0
	edge := 0
	nextEdge := 0
	last := 0

	var lineStarts []int

	for !t.IsComplete() {
		current := t.Current()

		if current < last {
			// dropping

			if nextEdge > edge {
				edge = nextEdge
				lineStarts = make([]int, edge)
				total += subTotal
				subTotal = 0
			}

			// (re-)set lineStarts
			for i := current + 1; i <= last; i++ {
				lineStarts[i-1] = t.idx
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
				subTotal += t.idx - lineStarts[i-1]
			}
		}

		last = current
		t.MoveNext()
	}

	return total + subTotal
}
